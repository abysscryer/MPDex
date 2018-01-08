using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 내역
    /// </summary>
    public class Statement : Creatable<long>
    {
        public StatementType StatementType { get; set; }

        public decimal Amount { get; set; }

        public decimal FeeAmount { get; set; }
        
        public string VerifyKey { get; set; }

        public short FeeId { get; set; }
        public virtual Fee Fee { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public short CoinId { get; set; }
        public virtual Coin Coin {get;set;}

    }
}
