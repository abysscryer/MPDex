using MPDex.Models.Base;
using System;

namespace MPDex.Models.ViewModels
{
    public class BookCreateViewModel
    {
        public BookType BookType { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public decimal Stock { get; set; }

        public Guid CustomerId { get; set; }

        public short CoinId { get; set; }
    }
}