using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MPDex.Data;
using MPDex.Models.Domain;

namespace MPDex.Services.EntityServices
{
    public class CustomerService : ICustomerService
    {
        private readonly MPDexDbContext context;
        private readonly ILogger logger;

        public CustomerService(MPDexDbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<IPagedList<Customer>> GetAsync(int pageIndex = 1, int pageSize = 20)
        {
            IPagedList<Customer> result;

            try
            {
                result = await context.Customer
                    .ToPagedListAsync(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, pageIndex, pageSize);
                throw;
            }

            return result;
        }

        public async Task<Customer> FindAsync(Guid id)
        {
            Customer result;
            try
            {
                result = await context.Customer.FindAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, id);
                throw;
            }

            return result;
        }

        public async Task<Guid> AddAsync(Customer entity)
        {
            Guid id;
            int effected;
            try
            {
                entity.Id = Guid.NewGuid();
                this.context.Customer.Add(entity);
                effected = await this.context.SaveChangesAsync();
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

        public async Task<bool> UpdateAsync(Customer entity)
        {
            int effected = 0;
            try
            {
                this.context.Update(entity);
                effected = await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, entity);
                throw;
            }

            return effected == 1;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var isSuccess = false;
            Customer entity;
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

        public async Task<bool> RemoveAsync(Customer entity)
        {
            int effected = 0;
            try
            {
                this.context.Update(entity);
                effected = await this.context.SaveChangesAsync();
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
