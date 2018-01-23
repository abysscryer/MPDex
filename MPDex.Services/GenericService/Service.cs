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
    public class Service<EM> : IService<EM>
        where EM : Entity
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly ILogger<Service<EM>> logger;
        protected readonly IRepository<EM> repository;

        public Service(IUnitOfWork unitOfWork, ILogger<Service<EM>> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.repository = unitOfWork.GetRepository<EM>();
        }

        public async Task<IPagedList<VM>> GetPagedListAsync<VM>(
            Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            int pageIndex = 0, int pageSize = 20, int indexFrom = 0, int itemCount = 0, bool disableTracking = true)
            where VM : class
        {
            return await this.repository.Get<VM>(selector: selector, predicate: predicate, orderBy: orderBy, include: include)
                .ToPagedListAsync(pageIndex, pageSize, indexFrom, itemCount);
        }

        public async Task<IEnumerable<VM>> Get<VM>(
            Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            bool disableTracking = true) 
            where VM : class
        {
            return await this.repository.Get<VM>(selector: selector, predicate: predicate, orderBy: orderBy, include: include)
                .ToListAsync();
        }

        public async Task<VM> FindAsync<VM>(Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            bool disableTracking = true) 
            where VM : class
        {
            return await this.repository.Get<VM>(selector: selector, predicate: predicate, orderBy: orderBy, include: include)
                .SingleOrDefaultAsync();
        }
        
        public virtual async Task<VM> FindAsync<VM>(params object[] keys)
            where VM : class
        {
            var vm = default(VM);
            var em = default(EM);

            try
            {
                em = await this.FindAsync(keys);
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

        public virtual async Task<VM> AddAsync<CM, VM>(CM cm)
            where CM : class
            where VM : class
        {
            var em = default(EM);
            var vm = default(VM);
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

        public virtual async Task<VM> UpdateAsync<UM, VM>(UM um, params object[] keys)
            where UM : class
            where VM : class
        {
            var ok = false;
            var target = default(EM);
            var source = default(EM);
            var vm = default(VM);

            try
            {
                target = await this.repository.FindAsync(keys);

                if (target != null)
                {
                    source = Mapper.Map(um, target);
                    ok = await this.UpdateAsync(source);
                }

                if (ok)
                    vm = Mapper.Map<VM>(source);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, um, target, source, ok);
                throw;
            }

            return vm;
        }

        public virtual async Task<bool> RemoveAsync(params object[] keys)
        {
            var ok = false;
            var em = default(EM);

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

        protected async Task<VM> MaxAsync<VM>(Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null)
            where VM : class
        {
            return await this.repository.Get<VM>(selector: selector, predicate: predicate)
                    .DefaultIfEmpty()
                    .MaxAsync();
        }
        
        protected async Task<EM> FindAsync(params object[] keys)
        {
            return await this.repository.FindAsync(keys);
        }

        protected async Task<EM> FirstAsync(
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            bool disableTracking = true)
        {
            return await this.repository.Get(predicate: predicate, orderBy: orderBy, include: include)
                .FirstOrDefaultAsync();
        }

        protected async Task<bool> AddAsync(EM em)
        {
            this.repository.Add(em);
            var effected = await this.unitOfWork.SaveChangesAsync();

            // create with balaces
            return effected > 0;
        }

        protected async Task<bool> UpdateAsync(EM em)
        {

            this.repository.Update(em);
            var effected = await this.unitOfWork.SaveChangesAsync();

            return effected == 1;
        }

        protected async Task<int> UpdateAsync(IEnumerable<EM> em)
        {
            this.repository.Update(em);
            return await this.unitOfWork.SaveChangesAsync();
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
