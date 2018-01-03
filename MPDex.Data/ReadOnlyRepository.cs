using Microsoft.EntityFrameworkCore;
using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MPDex.Data
{
    /// <summary>
    /// read only generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReadOnlyRepository<C, T> : _IReadOnlyRepository<T> 
        where T : BaseEntity 
        where C : DbContext
    {
        /// <summary>
        /// db context
        /// </summary>
        //protected readonly MPDexDbContext context;
        //private C context = new C();
        private C context;

        protected C Context
        {

            get { return this.context; }
            set { this.context = value; }
        }

        /// <summary>
        /// dbset entities
        /// </summary>
        private DbSet<T> entities;

        protected DbSet<T> Entities
        {

            get { return this.entities; }
            set { this.entities = value; }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        //public ReadOnlyRepository(C context, T entity)
        //{
        //    this.context = context;
        //    this.entities = context.Set<T>();
        //}
        public ReadOnlyRepository(C context)
        {
            this.context = context;
            this.entities = this.context.Set<T>();
        }

        //public ReadOnlyRepository()
        //{
        //    this.entities = this.context.Set<T>();
        //}

        /// <summary>
        /// make query
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> GetQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<T> query = this.entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        /// <summary>
        /// get all entities
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(null, orderBy, includeProperties, skip, take).ToList();
        }

        /// <summary>
        /// get all entites async
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <summary>
        /// get entities
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }

        /// <summary>
        /// get entities async
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <summary>
        /// get single entity
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T GetOne(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = "")
        {
            return GetQueryable(filter, null, includeProperties).SingleOrDefault();
        }

        /// <summary>
        /// get single entity async
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual async Task<T> GetOneAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null)
        {
            return await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        /// <summary>
        /// get first entity
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T GetFirst(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            return GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }

        /// <summary>
        /// get first entity async
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual async Task<T> GetFirstAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        /// <summary>
        /// find entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public virtual T GetById(object id)
        //{
        //    return this.entities.Find(id);
        //}

        /// <summary>
        /// find entity async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public virtual async Task<T> GetByIdAsync(object id)
        //{
        //    return await this.entities.FindAsync(id);
        //}

        /// <summary>
        /// count entities
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual int GetCount(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        /// <summary>
        /// count entities async
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetQueryable(filter).CountAsync();
        }

        /// <summary>
        /// is exists entity
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual bool GetExists(Expression<Func<T, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        /// <summary>
        /// is exists async
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetQueryable(filter).AnyAsync();
        }

        #region dispose

        /// <summary>
        /// dispose status
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// dispose overlaod
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
                this.disposed = true;
            }
        }

        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// finallizer
        /// </summary>
        ~ReadOnlyRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}
