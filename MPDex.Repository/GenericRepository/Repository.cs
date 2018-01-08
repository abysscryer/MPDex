using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPDex.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {

        protected readonly DbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            dbSet = this.dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get()
        {
            return this.dbSet;
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await this.dbSet.FindAsync(keyValues);
        }
        
        public void Add(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            this.dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }
    }
}
