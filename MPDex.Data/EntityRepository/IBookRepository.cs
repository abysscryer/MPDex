using MPDex.Models;
using System;
using System.Threading.Tasks;

namespace MPDex.Data
{
    public interface IBookRepository : IRepository<Book>
    {
        //Book GetById(Guid id);

        //Task<Book> GetByIdAsync(Guid id);
    }
}
