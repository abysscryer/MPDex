using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface ICoinService : IService<Coin, CoinCreateModel, CoinUpdateModel, CoinViewModel>
    {
        //Task<short> GetMaxAsync();
    }
}
