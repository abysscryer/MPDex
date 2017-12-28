using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    public class Coin : Entity<CoinType>
    {
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
