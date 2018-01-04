using Microsoft.EntityFrameworkCore;
using MPDex.Models;

namespace MPDex.Data
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(MPDexDbContext context) : base(context)
        { }
    }
}
