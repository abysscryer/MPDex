using MPDex.Models.Domain;
using System.Threading.Tasks;

namespace MPDex.Repository
{
    public interface ICoinRepository : IRepository<Coin>
    {
        Task<short> MaxAsync();
    }
}