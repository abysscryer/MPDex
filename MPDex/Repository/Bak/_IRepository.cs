using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MPDex.Domain;

namespace MPDex.Repository
{
    public interface _IRepository<T> where T : class, IEntity
    {
        T Get(object id);

        Task<T> GetAsync(object id);

        IQueryable<T> GetAll();

        Task<ICollection<T>> GetAllAsyn();

        T Find(Expression<Func<T, bool>> match);

        Task<T> FindAsync(Expression<Func<T, bool>> match);

        ICollection<T> FindAll(Expression<Func<T, bool>> match);

        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        T Add(T t);

        Task<T> AddAsyn(T t);
        
        void Save();

        Task<int> SaveAsync();

        T Update(T t, object key);

        Task<T> UpdateAsyn(T t, object key);

        void Delete(T entity);

        Task<int> DeleteAsyn(T entity);

        int Count();

        Task<int> CountAsync();

        void Dispose();
    }
}
