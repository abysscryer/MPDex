using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using MPDex.Models.Base;
using MPDex.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public class Service<EM, CM, UM, VM> : IService<EM, CM, UM, VM>
        where EM : Entity
        where CM : class
        where UM : class
        where VM : class
    {
        protected readonly IUnitOfWork unitOfWork;
        private readonly ILogger<Service<EM, CM, UM, VM>> logger;
        private readonly IRepository<EM> repository;

        public Service(IUnitOfWork unitOfWork, ILogger<Service<EM, CM, UM, VM>> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.repository = unitOfWork.GetRepository<EM>();
        }

        public async Task<IPagedList<VM>> GetPagedListAsync(
            Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            int pageIndex = 0, int pageSize = 20, int indexFrom = 0, int itemCount = 0, bool disableTracking = true)
        {
            return await this.repository.Get(selector:selector, predicate: predicate, orderBy: orderBy, include: include)
                .ToPagedListAsync(pageIndex, pageSize, indexFrom, itemCount);
        }

        public async Task<IEnumerable<VM>> Get(Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null
            , bool disableTracking = true)
        {
            return await this.repository.Get(selector: selector, predicate: predicate, orderBy: orderBy, include: include)
                .ToListAsync();
        }

        public async Task<VM> GetSingle(Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null
            , bool disableTracking = true)
        {
            return await this.repository.Get(selector: selector, predicate: predicate, orderBy: orderBy, include: include)
                .SingleOrDefaultAsync();
        }

        public async Task<VM> FindAsync(params object[] keys)
        {
            VM vm = default(VM);
            EM em = default(EM);

            try
            {
                em = await this.repository.FindAsync(keys);
                if (em != null)
                    vm = Mapper.Map<VM>(em);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, keys, em, vm);
                throw;
            }
            return vm;
        }
        
        public virtual async Task<VM> AddAsync(CM cm)
        {
            EM em = default(EM);
            VM vm = default(VM);
            var ok = false;
            
            try
            {
                em = Mapper.Map<EM>(cm);
                ok = await this.AddAsync(em);

                if (ok)
                    vm = Mapper.Map<VM>(em);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, cm, em, vm);
                throw;
            }

            return vm;
        }

        public async Task<VM> UpdateAsync(UM cm, params object[] keys)
        {
            var ok = false;
            EM target = default(EM);
            EM source = default(EM);
            VM vm = default(VM);

            try
            {
                target = await this.repository.FindAsync(keys);

                if (target != null)
                {
                    source = Mapper.Map(cm, target);
                    ok = await this.UpdateAsync(source);
                }

                if (ok)
                    vm = Mapper.Map<VM>(source);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, cm, target, source, ok);
                throw;
            }

            return vm;
        }
        
        public async Task<bool> RemoveAsync(params object[] keys)
        {
            var ok = false;
            EM em = null;
            try
            {
                em = await this.repository.FindAsync(keys);
                if (em != null)
                    ok = await this.RemoveAsync(em);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, em, ok);
                throw;
            }

            return ok;
        }

        #region protected methods
        
        protected async Task<bool> AddAsync(EM em)
        {
            this.repository.Add(em);
            var effected = await this.unitOfWork.SaveChangesAsync();

            return effected == 1;
        }

        protected async Task<bool> UpdateAsync(EM em)
        {

            this.repository.Update(em);
            var effected = await this.unitOfWork.SaveChangesAsync();

            return effected == 1;
        }

        protected async Task<bool> RemoveAsync(EM em)
        {
            this.repository.Remove(em);
            var effected = await this.unitOfWork.SaveChangesAsync();

            return effected == 1;
        }
        
        #endregion
    }
}
