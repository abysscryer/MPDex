using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MPDex.Models.Base;
using MPDex.Models.Domain;
using MPDex.Repository;

namespace MPDex.Services
{
    public class CustomerService : Service<Customer>, ICustomerService
    {
        
        private readonly ICustomerRepository repository;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(IUnitOfWork unitOfWork, ILogger<CustomerService> logger, ILogger<Service<Customer>> genericLogger)
            : base(unitOfWork, genericLogger)
        {
            this.repository = unitOfWork.CustomerRepository;
            this.logger = logger;
        }
    }
}
