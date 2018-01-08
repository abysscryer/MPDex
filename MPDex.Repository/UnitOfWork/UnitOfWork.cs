using Microsoft.EntityFrameworkCore;
using MPDex.Models.Base;
using MPDex.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPDex.Repository
{
    public class UnitOfWork<TContext>  : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork
        where TContext : DbContext
    {
        private bool disposed = false;
        private Dictionary<Type, object> repositories;
        private readonly TContext context;
        
        public UnitOfWork(TContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the db context.
        /// </summary>
        /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
        public TContext DbContext => this.context;

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new Repository<TEntity>(this.context);
            }

            //if (type.Equals(typeof(Customer)))
            //    return (ICustomerRepository)repositories[type];

            return (IRepository< TEntity>)repositories[type];
        }

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the database.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if save changes ensure auto record the change history.</param>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
        {
            if (ensureAutoHistory)
            {
                //this.context.EnsureAutoHistory();
            }

            return await this.context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
        {
            throw new NotImplementedException();
        }
        
        public int SaveChanges(bool ensureAutoHistory = false)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            throw new NotImplementedException();
        }

        #region Dispose

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // clear repositories
                    if (repositories != null)
                    {
                        repositories.Clear();
                    }

                    // dispose the db context.
                    this.context.Dispose();
                }
            }

            disposed = true;
        }
        
        #endregion
    }
}
