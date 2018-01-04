using Microsoft.EntityFrameworkCore;
using MPDex.Models;

namespace MPDex.Data
{
    public interface IBookRepository : IRepository<Book>
    { }
}
