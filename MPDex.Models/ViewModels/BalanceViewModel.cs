using System;
using System.ComponentModel.DataAnnotations;

namespace MPDex.Models.ViewModels
{
    public class BalanceCreateModel
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public short CoinId { get; set; }
    }

    public class BalanceUpdateModel
    {
        [Range(0.0, double.MaxValue)]
        public decimal CurrentAmount { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal BookAmount { get; set; }
    }

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
