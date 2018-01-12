using MPDex.Models.Domain;
using MPDex.Models.ViewModels;

namespace MPDex.Services
{
    public interface ICustomerService : IService<Customer, CustomerCreateModel, CustomerUpdateModel, CustomerViewModel>
    { }
}