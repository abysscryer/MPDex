using Microsoft.EntityFrameworkCore.Query;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface ICoinService : IService<Coin, CoinCreateModel, CoinUpdateModel, CoinViewModel>
    {
        Task<short> GetMaxAsync();
    }
}
