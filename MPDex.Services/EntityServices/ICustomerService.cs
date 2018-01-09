using MPDex.Models.Base;
using MPDex.Models.Domain;
using System;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface ICustomerService : IService<Customer>
    {
        //Task<Customer> FindAsync(Guid id);
        //Task<bool> RemoveAsync(Guid id);
    }
}