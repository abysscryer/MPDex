using Microsoft.Extensions.Logging;
using MPDex.Repository;
using MPDex.Models.Domain;
using System;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public class CoinService : ICoinService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly CoinRepository coins;
        private readonly ILogger<CoinService> logger;
        
        public CoinService(IUnitOfWork unitOfWork, ILogger<CoinService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.coins = (CoinRepository)unitOfWork.GetRepository<Coin>();
            this.logger = logger;
        }

        public async Task<IPagedList<Coin>> GetAsync(int pageIndex = 1, int pageSize = 20)
        {
            IPagedList<Coin> result;

            try
            {
                result = await this.coins.Get().ToPagedListAsync(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, pageIndex, pageSize);
                throw;
            }

            return result;
        }

        public async Task<Coin> FindAsync(short id)
        {
            Coin result;
            try
            {
                result = await this.coins.FindAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, id);
                throw;
            }

            return result;
        }

        public async Task<short> Max()
        {
            short max;

            try
            {
                max = await coins.MaxAsync();
            }
            catch (Exception ex )
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

            return max;
        }

        public async Task<short> AddAsync(Coin entity)
        {
            short id = 0;
            int effected;
            try
            {
                var max = await this.Max();
                entity.Id = Convert.ToInt16(max + 1);
                this.coins.Add(entity);
                effected = await this.unitOfWork.SaveChangesAsync();
                if (effected == 1)
                    id = entity.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, entity, id);
                throw;
            }

            return id;
        }

        public async Task<bool> UpdateAsync(Coin entity)
        {
            int effected = 0;
            try
            {
                this.coins.Update(entity);
                effected = await this.unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, entity);
                throw;
            }

            return effected == 1;
        }

        public async Task<bool> RemoveAsync(short id)
        {
            var isSuccess = false;
            Coin entity;
            try
            {
                entity = await this.FindAsync(id);
                isSuccess = await this.RemoveAsync(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, id);
                throw;
            }

            return isSuccess;
        }

        public async Task<bool> RemoveAsync(Coin entity)
        {
            int effected = 0;
            try
            {
                this.coins.Update(entity);
                effected = await this.unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, entity);
                throw;
            }

            return effected == 1;
        }
    }
}
