using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;

namespace MPDex.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}
