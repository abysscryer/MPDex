using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 계약
    /// </summary>
    public class Contract : Entity<long>
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        public long TradeId { get; set; }
        public virtual Trade Trade { get; set; }
    }
}
