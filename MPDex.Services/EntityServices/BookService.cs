using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MPDex.Data;
using MPDex.Models.Domain;

namespace MPDex.Services.EntityServices
{
    public class BookService : IBookService
    {
        private readonly MPDexDbContext context;
        private readonly ILogger logger;

        public BookService(MPDexDbContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }
        
        public async Task<IPagedList<Book>> GetAsync(int pageIndex, int pageSize)
        {
            IPagedList<Book> result;

            try
            {
                result = await context.Book
                    .ToPagedListAsync(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, pageIndex, pageSize);
                throw;
            }

            return result;
        }
        
        public async Task<Book> FindAsync(Guid id)
        {
            Book result;
            try
            {
                result = await context.Book.FindAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, id);
                throw;
            }

            return result;
        }

        public async Task<Guid> AddAsync(Book entity)
        {
            Guid id;
            int effected;
            try
            {
                entity.Id = Guid.NewGuid();
                this.context.Book.Add(entity);
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

        public async Task<bool> UpdateAsync(Book entity)
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
            Book entity;
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

        public async Task<bool> RemoveAsync(Book entity)
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
