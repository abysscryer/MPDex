using Microsoft.Extensions.Logging;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Repository;
using System;
using System.Threading.Tasks;

namespace MPDex.Services
{
    //public interface ICoinService : IService<Coin, CoinCreateModel, CoinCreateModel, CoinViewModel>
    //{ }

    public interface ICoinService : IService<Coin>
    {
        Task<CoinViewModel> AddAsync(CoinCreateModel cm);
    }

    public class CoinService : Service<Coin>, ICoinService
    {
        public CoinService(IUnitOfWork unitOfWork, 
                           ILogger<Service<Coin>> logger)
            : base(unitOfWork, logger)
        {}
        
        public async Task<CoinViewModel> AddAsync(CoinCreateModel cm)
        {
            var max = await base.MaxAsync(x => new CoinViewModel { Id =  x.Id });
            cm.Id = (short)(max==null? 1:++max.Id);

            return await base.AddAsync<CoinCreateModel, CoinViewModel>(cm);
        }
    }
}
