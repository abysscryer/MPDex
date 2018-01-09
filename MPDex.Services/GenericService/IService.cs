using MPDex.Models.Base;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface IService
    {
    }

    public interface IService<TEntity> : IService
        where TEntity : Entity
    {
        Task<IPagedList<TEntity>> GetAsync(int pageIndex, int pageSize);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> RemoveAsync(TEntity entity);
        Task<bool> RemoveAsync(params object[] keyValues);
    }
}
