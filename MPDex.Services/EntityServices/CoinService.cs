﻿using Microsoft.Extensions.Logging;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Repository;
using System;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public class CoinService : Service<Coin, CoinCreateModel, CoinUpdateModel, CoinViewModel>, ICoinService
    {
        private readonly ICoinRepository repository;
        private readonly ILogger<CoinService> logger;

        public CoinService(IUnitOfWork unitOfWork, 
                           ILogger<CoinService> logger, 
                           ILogger<Service<Coin, CoinCreateModel, CoinUpdateModel, CoinViewModel>> genericLogger)
            : base(unitOfWork, genericLogger)
        {
            this.repository = unitOfWork.CoinRepository;
            this.logger = logger;
        }
        
        //public async Task<short> GetMaxAsync()
        //{
        //    short max;

        //    try
        //    {
        //        max = await this.repository.MaxAsync();
        //    }
        //    catch (Exception ex )
        //    {
        //        logger.LogError(ex, ex.Message);
        //        throw;
        //    }

        //    return max;
        //}

        public override async Task<CoinViewModel> AddAsync(CoinCreateModel cm)
        {
            var max = await base.MaxAsync(x => new CoinViewModel { Id = x.Id });
            //var id = await this.GetMaxAsync();
            //cm.Id = ++id;
            var id = max.Id;

            return await base.AddAsync(cm);
        }
    }
}
