using MPDex.Models.Domain;
using System;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface IBookService
    {
        Task<IPagedList<Book>> GetAsync(int pageIndex, int pageSize);
        Task<Book> FindAsync(Guid id);
        Task<Guid> AddAsync(Book entity);
        Task<bool> UpdateAsync(Book entity);
        Task<bool> RemoveAsync(Guid id);
        Task<bool> RemoveAsync(Book entity);
    }
}
