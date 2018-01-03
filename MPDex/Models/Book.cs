using MPDex.Models.Base;
using System;

namespace MPDex.Models
{
    public class Book : Auditable<Guid>
    {
        public Book()
        { }
        
        public BookType BookType { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public decimal Stock { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual Coin Coin { get; set; }
        
    }
}

