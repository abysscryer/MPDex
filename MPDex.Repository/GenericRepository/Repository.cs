using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MPDex.Repository
{
    public class Repository<EM> : IRepository<EM>
        where EM : Entity
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<EM> dbSet;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{EntityModel}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            dbSet = this.dbContext.Set<EM>();
        }

        public DbSet<EM> Entitis
        {
            get { return this.dbSet; }
        }

        public DbContext Context
        {
            get { return this.dbContext; }
        }

        /// <summary>
        /// Gets the <see cref="IPagedList{ViewModel}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.
        /// </summary>
        /// <param name="selector">The selector for projection.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="pageIndex">The index of page.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>An <see cref="IPagedList{ViewModel}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        public IQueryable<EM> Get(
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true)
        {
            IQueryable<EM> query = this.dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public IQueryable<VM> Get<VM>(Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true)
            where VM : class
        {
            IQueryable<EM> query = this.dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            
            return query.Select(selector);
        }

        public async Task<EM> FindAsync(params object[] keys)
        {
            return await this.dbSet.FindAsync(keys);
        }

        public void Add(EM em) => this.dbSet.Add(em);

        public void Add(IEnumerable<EM> em) => this.dbSet.AddRange(em);

        public void Update(EM em) => this.dbSet.Update(em);
        
        public void Update(IEnumerable<EM> em) => this.dbSet.UpdateRange(em);

        public void Remove(EM em) => this.dbSet.Remove(em);

        public void Remove(IEnumerable<EM> em) => this.dbSet.RemoveRange(em);

        /// <summary>
        /// Gets the count based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<EM, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return this.dbSet.Count();
            }
            else
            {
                return this.dbSet.Count(predicate);
            }
        }

        /// <summary>
        /// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
        /// </summary>
        /// <param name="sql">The raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
        //public IQueryable<EM> FromSql(string sql, params object[] parameters) => this.dbSet.FromSql(sql, parameters);

    }
}
