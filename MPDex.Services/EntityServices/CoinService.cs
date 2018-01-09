using Microsoft.Extensions.Logging;
using MPDex.Repository;
using MPDex.Models.Domain;
using System;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public class CoinService : Service<Coin>, ICoinService
    {
        private readonly ICoinRepository repository;
        private readonly ILogger<CoinService> logger;

        public CoinService(IUnitOfWork unitOfWork, ILogger<CoinService> logger , ILogger<Service<Coin>> genericLogger)
            : base(unitOfWork, genericLogger)
        {
            this.repository = unitOfWork.CoinRepository;
            this.logger = logger;
        }
        
        public async Task<short> GetMaxAsync()
        {
            short max;

            try
            {
                max = await this.repository.MaxAsync();
            }
            catch (Exception ex )
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

            return max;
        }
    }
}
