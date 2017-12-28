﻿using MPDex.Domain.Base;
using System.Collections.Generic;

namespace MPDex.Domain
{
    public class Coin : Entity<CoinType>
    {
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
