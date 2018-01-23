using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    public class Fee : Creatable<short>
    {
        public decimal Percent { get; set; }

        public short CoinId { get; set; }
        public virtual Coin Coin { get; set; }

        public virtual ICollection<Statement> Statements { get; set; }
    }
}
