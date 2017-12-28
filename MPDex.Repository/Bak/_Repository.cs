using Microsoft.EntityFrameworkCore;
using MPDex.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MPDex.Repository
{
    public abstract class _Repository<T> : _IRepository<T> where T : class, IEntity
    {
        private readonly MPDexContext context;
        private readonly DbSet<T> entities;

        public _Repository(MPDexContext context)
        {
            this.context = context;
            this.entities = this.context.Set<T>();
        }
        
        public virtual T Get(object id)
        {
            return this.entities.Find(id);
        }

        public virtual async Task<T> GetAsync(object id)
        {
            return await this.entities.FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await this.entities.ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return this.entities.SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await this.entities.SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return this.entities.Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await this.entities.Where(match).ToListAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = this.entities.Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await this.entities.Where(predicate).ToListAsync();
        }
        
        public int Count()
        {
            return this.entities.Count();
        }

        public async Task<int> CountAsync()
        {
            return await this.entities.CountAsync();
        }

        public virtual T Add(T entity)
        {
            this.entities.Add(entity);
            this.context.SaveChanges();
            return entity;
        }

        public virtual async Task<T> AddAsyn(T entity)
        {
            this.entities.Add(entity);
            await this.context.SaveChangesAsync();
            return entity;
        }
        
        public virtual T Update(T entity, object key)
        {
            if (entity == null)
                return null;
            T exist = this.entities.Find(key);
            if (exist != null)
            {
                this.context.Entry(exist).CurrentValues.SetValues(entity);
                this.context.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsyn(T entity, object key)
        {
            if (entity == null)
                return null;
            T exist = await this.entities.FindAsync(key);
            if (exist != null)
            {
                this.context.Entry(exist).CurrentValues.SetValues(entity);
                await this.context.SaveChangesAsync();
            }
            return exist;
        }

        public virtual void Delete(T entity)
        {
            this.entities.Remove(entity);
            this.context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            this.entities.Remove(entity);
            return await this.context.SaveChangesAsync();
        }
        
        public virtual void Save()
        {
            this.context.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await this.context.SaveChangesAsync();
        }
        
        #region dispose

        private bool disposed = false;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
