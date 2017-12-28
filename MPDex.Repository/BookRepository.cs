using MPDex.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(MPDexContext context) : base(context)
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
