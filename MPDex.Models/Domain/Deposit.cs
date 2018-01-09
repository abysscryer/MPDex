using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 예치금
    /// </summary>
    public class Deposit : Entity
    {
        public decimal Amount { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public short CoinId { get; set; }
        public virtual Coin Coin { get; set; }
    }
}
