using System;

namespace MPDex.Models.ViewModels
{
    public class BalanceViewModel
    {
        public Guid CustomerId { get; set; }

        public short CoinId { get; set; }

        public decimal CurrentAmount { get; set; }

        public decimal BookAmount { get; set; }

        public virtual string NickName { get; set; }

        public virtual short CoinName { get; set; }
    }
}
