using MPDex.Models.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MPDex.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();
        Task<TEntity> FindAsync(object[] keyValues);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }

    public interface IRepository { }
}
