using MPDex.Models.ViewModels;
using System;

namespace MPDex.Services
{
    public class BookChangedEventArgs : EventArgs
    {
        private BookCacheModel book;
        private bool isIncrease;

        public BookChangedEventArgs()
        { }

        public BookChangedEventArgs(BookCacheModel book, bool isIecrease=true)
        {
            this.book = book;
            this.isIncrease = isIecrease;
        }

        public BookCacheModel Book
        {
            get { return book; }
            set { book = value; }
        }

        public bool IsIncrease
        {
            get { return isIncrease; }
            set { isIncrease = value; }
        }
    }
}
