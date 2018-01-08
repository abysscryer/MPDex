using MPDex.Models.Domain;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface ICoinService : IEntityService
    {
        Task<IPagedList<Coin>> GetAsync(int pageIndex, int pageSize);
        Task<Coin> FindAsync(short id);
        Task<short> Max();
        Task<short> AddAsync(Coin entity);
        Task<bool> UpdateAsync(Coin entity);
        Task<bool> RemoveAsync(short id);
        Task<bool> RemoveAsync(Coin entity);
    }
}
