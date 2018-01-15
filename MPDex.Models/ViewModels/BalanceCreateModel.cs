using MPDex.Models.Base;
using System;
using System.Collections.Generic;

namespace MPDex.Models.ViewModels
{
    public class BalanceCreateModel
    {
        public Guid CustomerId { get; set; }

        public short CoinId { get; set; }
    }

    public class StatementCreateModel
    {
        public long Id { get; set; }

        public StatementType StatementType { get; set; }

        public decimal Amount { get; set; }

        public decimal FeeAmount { get; set; }

        public string VerifyKey { get; set; }

        //public short FeeId { get; set; }
        public virtual short FeeId { get; set; }

        //public Guid CustomerId { get; set; }
        public virtual string CustomerId { get; set; }

        //public short CoinId { get; set; }
        public virtual short CoinId { get; set; }
    }

    public class StatementViewModel
    {
        public long Id { get; set; }

        public StatementType StatementType { get; set; }

        public decimal Amount { get; set; }

        public decimal FeeAmount { get; set; }

        public string VerifyKey { get; set; }

        //public short FeeId { get; set; }
        public virtual short FeeId { get; set; }

        //public Guid CustomerId { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual string NickName { get; set; }

        //public short CoinId { get; set; }
        public virtual short CoinId { get; set; }
        public virtual string CoinName { get; set; }
    }

    public class TradeCreateModel
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        //public short CoinId { get; set; }
        public virtual short CoinId { get; set; }

        public virtual ICollection<ContractCreateModel> Contracts { get; set; }
    }

    public class TradeViewModel
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        //public short CoinId { get; set; }
        public virtual short CoinId { get; set; }
        public virtual string CoinName { get; set; }

        public virtual ICollection<ContractViewModel> Contracts { get; set; }
    }
}
