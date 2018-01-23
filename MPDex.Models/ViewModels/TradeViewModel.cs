using System;
using System.Collections.Generic;

namespace MPDex.Models.ViewModels
{
    public class TradeCreateModel
    {
        public TradeCreateModel()
        {
            this.Contracts = new List<ContractCreateModel>();
        }

        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public virtual short CoinId { get; set; }

        public virtual ICollection<ContractCreateModel> Contracts { get; set; }
    }

    public class TradeViewModel
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        
        public virtual short CoinId { get; set; }
        public virtual string CoinName { get; set; }

        public virtual ICollection<ContractViewModel> Contracts { get; set; }
    }
}
