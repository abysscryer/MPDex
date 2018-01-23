using System;
using System.ComponentModel.DataAnnotations;

namespace MPDex.Models.ViewModels
{
    public class FeeCreateModel
    {
        public short Id { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Percent { get; set; }

        [Required, Range(1, short.MaxValue)]
        public virtual short CoinId { get; set; }
    }

    public class FeeViewModel
    {
        public short Id { get; set; }

        public decimal Percent { get; set; }

        public virtual string CoinName { get; set; }

        public DateTime OnCreated { get; set; }
    }
}
