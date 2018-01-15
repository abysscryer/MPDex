using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    public class Trade : Creatable<Guid>
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        
        public virtual Coin Coin { get; set; }
        
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
