using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    public class Trade : Creatable<Guid>
    {
        public TradeType TradeType { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        
        public short CoinId { get; set; }
        public virtual Coin Coin { get; set; }

        public short CurrencyId { get; set; }
        public virtual Coin Currency { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
