using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MPDex.Repository
{
    public interface IRepository<TEntity> 
        where TEntity : Entity
    {
        IQueryable<TEntity> Get();
        IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindAsync(params object[] keyValues);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }

    public interface IRepository { }
}
