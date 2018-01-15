using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 내역
    /// </summary>
    public class Statement : Auditable<Guid>
    {
        public StatementType StatementType { get; set; }
        public decimal BeforeAmount { get; set; }
        public decimal AfterAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal FeeAmount { get; set; }
        public string VerifyKey { get; set; }

        public virtual Order Order { get; set; }
        public virtual Fee Fee { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Coin Coin {get;set;}
        public virtual Balance Balance { get; set; }
    }
}
