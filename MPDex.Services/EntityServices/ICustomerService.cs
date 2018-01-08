using MPDex.Models.Domain;
using System;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface ICustomerService
    {
        Task<IPagedList<Customer>> GetAsync(int pageIndex, int pageSize);
        Task<Customer> FindAsync(Guid id);
        Task<Guid> AddAsync(Customer entity);
        Task<bool> UpdateAsync(Customer entity);
        Task<bool> RemoveAsync(Guid id);
        Task<bool> RemoveAsync(Customer entity);
    }
}