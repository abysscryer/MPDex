using MPDex.Models.Base;
using MPDex.Models.Domain;
using System;

namespace MPDex.Models.ViewModels
{
    public class OrderCreateModel
    {
        public Guid Id { get; set; }

        public OrderType OrderType { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public decimal Stock { get; set; }

        public short CoinId { get; set; }

        public virtual Guid CustomerId { get; set; }

        public virtual Guid ContractId { get; set; }

        // Book
        public Guid BookId { get; set; }
        public byte OrderCount { get; set; }
    }

    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Stock { get; set; }

        public Guid BookId { get; set; }
        public short OrderCount { get; set; }

        public DateTime OnCreated { get; set; }

        public virtual Guid CustomerId { get; set; }
        public virtual string NickName { get; set; }

        public virtual short CoinId { get; set; }
        public virtual string CoinName { get; set; }

        public virtual long ContractId { get; set; }
    }
}
