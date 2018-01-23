using MPDex.Models.Base;
using System;
using System.Collections.Generic;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 잔고
    /// </summary>
    public class Balance : Entity
    {
        public decimal Amount { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public short CoinId {get;set;}
        public virtual Coin Coin { get; set; }
        
        public virtual ICollection<Statement> Statements { get; set; }
    }
}
