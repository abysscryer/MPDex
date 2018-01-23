using AutoMapper;
using Microsoft.Extensions.Logging;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface IFeeService : IService<Fee>
    {
        Task<FeeViewModel> AddAsync(FeeCreateModel cm);
    }

    public class FeeService : Service<Fee>, IFeeService
    {
        public FeeService(IUnitOfWork unitOfWork,
                           ILogger<Service<Fee>> logger)
            : base(unitOfWork, logger)
        {}

        public async Task<FeeViewModel> AddAsync(FeeCreateModel cm)
        {
            try
            {
                var max = await base.MaxAsync(x => new FeeViewModel { Id = x.Id });
                cm.Id = (short)(max == null ? 1 : ++max.Id);

                var em = Mapper.Map<Fee>(cm);
                var ok = await base.AddAsync(em);

                if (ok)
                    return await base.FindAsync(x => new FeeViewModel
                    {
                        Id = em.Id,
                        Percent = em.Percent,
                        CoinName = x.Coin.Name,
                        OnCreated = x.OnCreated
                    }, predicate: x => x.Id == em.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, cm);
                throw;
            }

            return new FeeViewModel();

        }
    }
}
