using MPDex.Models.Base;
using System;
using System.Collections.Generic;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 코인
    /// </summary>
    public class Coin : Creatable<short>
    {
        public string Name { get; set; }

        public virtual ICollection<Balance> Balances { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Fee> Fees { get; set; }
        public virtual ICollection<Statement> Statements { get; set; }
    }
}
