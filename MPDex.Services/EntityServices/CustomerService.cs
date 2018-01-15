using Microsoft.Extensions.Logging;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Repository;
using System;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public class CustomerService : Service<Customer, CustomerCreateModel, CustomerUpdateModel, CustomerViewModel>, ICustomerService
    {

        private readonly ICustomerRepository repository;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(IUnitOfWork unitOfWork, 
                               ILogger<CustomerService> logger, 
                               ILogger<Service<Customer, CustomerCreateModel, CustomerUpdateModel, CustomerViewModel>> genericLogger)
            : base(unitOfWork, genericLogger)
        {
            this.repository = unitOfWork.CustomerRepository;
            this.logger = logger;
        }

        public override async Task<CustomerViewModel> AddAsync(CustomerCreateModel cm)
        {
            cm.Id = Guid.NewGuid();

            return await base.AddAsync(cm);
        }
    }
}
