using MPDex.Models;

namespace MPDex.Data
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(MPDexDbContext context) : base(context)
        { }

        #region dispose

        /// <summary>
        /// dispose status
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
                this.disposed = true;
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// finallizer
        /// </summary>
        ~BookRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}
