﻿using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    public class Trade : Creatable<long>
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        public short CoinId { get; set; }
        public virtual Coin Coin { get; set; }
        
        public virtual ICollection<Contract> Contract { get; set; }
    }
}