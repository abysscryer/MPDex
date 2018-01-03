using Microsoft.Extensions.Logging;
using MPDex.Data;
using MPDex.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public class CoinService : ICoinService
    {
        private readonly ICoinRepository repository;
        private readonly ILogger logger;

        public CoinService(ICoinRepository coinRepository, ILogger logger)
        {
            this.repository = coinRepository;
            this.logger = logger;
        }
    }
}
