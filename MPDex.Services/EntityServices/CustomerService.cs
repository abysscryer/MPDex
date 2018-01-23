using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface ICustomerService : IService<Customer>
    {
        Task<CustomerViewModel> AddAsync(CustomerCreateModel cm);
    }

    public class CustomerService : Service<Customer>, ICustomerService
    {
        private IRepository<Coin> coinRepository;
        
        public CustomerService(IUnitOfWork unitOfWork,
                               ILogger<Service<Customer>> logger)
            : base(unitOfWork, logger)
        {
            this.coinRepository = unitOfWork.GetRepository<Coin>();
        }

        public async Task<CustomerViewModel> AddAsync(CustomerCreateModel cm)
        {
            cm.Id = Guid.NewGuid();
            
            var coins = await coinRepository.Get(x => new CoinViewModel { Id = x.Id }).ToListAsync();
            cm.Balances = new List<Balance>();
            coins.ForEach(x => cm.Balances.Add(new Balance { CoinId = x.Id, CustomerId = cm.Id }));
            
            return await base.AddAsync<CustomerCreateModel, CustomerViewModel>(cm);
        }
    }
}
