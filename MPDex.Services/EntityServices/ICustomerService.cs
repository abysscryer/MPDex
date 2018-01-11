using MPDex.Models.Base;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface ICustomerService : IService<Customer, CustomerCreateModel, CustomerUpdateModel, CustomerViewModel>
    { }
}