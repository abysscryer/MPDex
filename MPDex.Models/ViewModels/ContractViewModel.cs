using System;
using System.Collections.Generic;

namespace MPDex.Models.ViewModels
{
    public class ContractCreateModel
    {
        public ContractCreateModel(Guid tradeId)
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid TradeId { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<OrderCreateModel> Orders { get; set; }
    }

    public class ContractViewModel
    {
        public long Id { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<OrderCreateModel> Orders { get; set; }
    }
}
