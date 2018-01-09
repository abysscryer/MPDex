using MPDex.Models.Domain;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface ICoinService : IService<Coin>
    {
        Task<short> GetMaxAsync();
    }
}
