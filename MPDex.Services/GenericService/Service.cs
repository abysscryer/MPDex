using Microsoft.Extensions.Logging;
using MPDex.Models.Base;
using MPDex.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public class Service<TEntity> : IService<TEntity>
         where TEntity : Entity
    {
        protected readonly IUnitOfWork unitOfWork;
        private readonly ILogger<Service<TEntity>> logger;
        private readonly IRepository<TEntity> repository;

        public Service(IUnitOfWork unitOfWork, ILogger<Service<TEntity>> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.repository = unitOfWork.GetRepository<TEntity>();
        }
        
        public async Task<IPagedList<TEntity>> GetAsync(int pageIndex, int pageSize)
        {
            IPagedList<TEntity> result;

            try
            {
                result = await this.repository.Get().ToPagedListAsync(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, pageIndex, pageSize);
                throw;
            }

            return result;
        }
        
        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            TEntity entity = null;
            try
            {
                entity = await this.repository.FindAsync(keyValues);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, keyValues);
                throw;
            }
            return entity;
        }

        public async virtual Task<bool> AddAsync(TEntity entity)
        {
            var effected = 0;
            try
            {   
                this.repository.Add(entity);
                effected = await this.unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, entity, effected);
                throw;
            }

            return effected == 1;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var effected = 0;
            try
            {
                this.repository.Update(entity);
                effected = await this.unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, entity, effected);
                throw;
            }

            return effected == 1;
        }
        
        public async Task<bool> RemoveAsync(TEntity entity)
        {
            var effected = 0;
            try
            {
                this.repository.Remove(entity);
                effected = await this.unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, entity, effected);
                throw;
            }

            return effected == 1;
        }

        public async Task<bool> RemoveAsync(params object[] keyValues)
        {
            var isSuccess = false;
            TEntity entity = null;
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
    }
}
