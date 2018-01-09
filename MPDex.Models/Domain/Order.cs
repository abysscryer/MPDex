using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 주문
    /// </summary>
    public class Order : Auditable<long>
    {
        public OrderType OrderType { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Stock { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public short CoinId { get; set; }
        public virtual Coin Coin { get; set; }

        //public long StatementId { get; set; }
        public virtual Statement Statement { get; set; }

        public long ContractId { get; set; }
        public virtual Contract Contract { get; set; }
    }
}
