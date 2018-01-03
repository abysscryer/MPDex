using MPDex.Models;
using System;
using System.Threading.Tasks;

namespace MPDex.Data
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(MPDexDbContext context) : base(context)
        { }

        /// <summary>
        /// find entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public Book GetById(Guid id)
        //{
        //    return this._dbSet.Find(id);
        //}

        /// <summary>
        /// find entity async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public async Task<Book> GetByIdAsync(Guid id)
        //{
        //    return await this._dbSet.FindAsync(id);
        //}
        
    }
}
