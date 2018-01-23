using MPDex.Models.Base;
using System;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 예약
    /// </summary>
    public class Book : Auditable<Guid>
    {
        public BookType BookType { get; set; }

        public BookStatus BookStatus { get; set; }

        public byte OrderCount { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public decimal Stock { get; set; }
        
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
        public short CoinId { get; set; }
        public virtual Coin Coin { get; set; }

        public short CurrencyId { get; set; }
        public virtual Coin Currency { get; set; }
    }
}