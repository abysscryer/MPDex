using Microsoft.Extensions.Logging;
using MPDex.Repository;
using MPDex.Models.Domain;
using System;
using System.Threading.Tasks;
using MPDex.Models.ViewModels;
using AutoMapper;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;

namespace MPDex.Services
{
    public class CoinService : Service<Coin, CoinCreateModel, CoinUpdateModel, CoinViewModel>, ICoinService
    {
        private readonly ICoinRepository repository;
        private readonly ILogger<CoinService> logger;

        public CoinService(IUnitOfWork unitOfWork, ILogger<CoinService> logger, 
                           ILogger<Service<Coin, CoinCreateModel, CoinUpdateModel, CoinViewModel>> genericLogger)
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

        public override async Task<CoinViewModel> AddAsync(CoinCreateModel cModel)
        {
            var id = await this.GetMaxAsync();
            cModel.Id = (short)++id;

            return await base.AddAsync(cModel);
        }
    }
}
