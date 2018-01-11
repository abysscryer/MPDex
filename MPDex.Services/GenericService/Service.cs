using AutoMapper;
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
            return await this.repository.Get<VM>(selector:selector, predicate: predicate, orderBy: orderBy, include: include)
                .ToPagedListAsync(pageIndex, pageSize, indexFrom, itemCount);
        }
        
        public async Task<VM> FindAsync(params object[] keys)
        {
            VM vModel = default(VM);
            EM entity = default(EM);

            try
            {
                entity = await this.repository.FindAsync(keys);
                if (entity != null)
                    vModel = Mapper.Map<VM>(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, keys, entity, vModel);
                throw;
            }
            return vModel;
        }
        
        public virtual async Task<VM> AddAsync(CM cModel)
        {
            EM entity = default(EM);
            VM vModel = default(VM);
            var isSuccess = false;
            
            try
            {
                entity = Mapper.Map<EM>(cModel);
                isSuccess = await this.AddAsync(entity);

                if (isSuccess)
                    vModel = Mapper.Map<VM>(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, cModel, entity, vModel);
                throw;
            }

            return vModel;
        }
        
        public async Task<VM> UpdateAsync(UM cModel, params object[] keys)
        {
            var isSuccess = false;
            EM target = default(EM);
            EM source = default(EM);
            VM viewModel = default(VM);

            try
            {
                target = await this.repository.FindAsync(keys);

                if (target != null)
                {
                    source = Mapper.Map(cModel, target);
                    isSuccess = await this.UpdateAsync(source);
                }

                if (isSuccess)
                    viewModel = Mapper.Map<VM>(source);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, cModel, target, source, isSuccess);
                throw;
            }

            return viewModel;
        }
        
        public async Task<bool> RemoveAsync(params object[] keyValues)
        {
            var isSuccess = false;
            EM entity = null;
            try
            {
                entity = await this.repository.FindAsync(keyValues);
                if (entity != null)
                    isSuccess = await this.RemoveAsync(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, entity, isSuccess);
                throw;
            }

            return isSuccess;
        }

        #region private methods
        
        private async Task<bool> AddAsync(EM entity)
        {
            this.repository.Add(entity);
            var effected = await this.unitOfWork.SaveChangesAsync();

            return effected == 1;
        }

        private async Task<bool> UpdateAsync(EM entity)
        {

            this.repository.Update(entity);
            var effected = await this.unitOfWork.SaveChangesAsync();

            return effected == 1;
        }

        private async Task<bool> RemoveAsync(EM entity)
        {
            this.repository.Remove(entity);
            var effected = await this.unitOfWork.SaveChangesAsync();

            return effected == 1;
        }
        
        #endregion
    }
}
