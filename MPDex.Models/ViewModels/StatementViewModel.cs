using MPDex.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MPDex.Models.ViewModels
{
    public class StatementCreateModel
    {
        public long Id { get; set; }

        [Required]
        public StatementType StatementType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal FeeAmount { get; set; }

        [Required]
        public string VerifyKey { get; set; }

        public virtual short FeeId { get; set; }

        [Required]
        public virtual string CustomerId { get; set; }

        [Required]
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
}
