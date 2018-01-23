using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 주문
    /// </summary>
    public class Order : Creatable<Guid>
    {
        public Order()
        { }

        public Order(Book book)
        {
            this.Id = new Guid();
            this.OrderType = (OrderType)book.BookType;
            this.OrderStatus = OrderStatus.Placed;

            this.Price = book.Price;
            this.Amount = book.Amount;
            this.Stock = book.Stock;

            this.BookId = book.Id;
            this.OrderCount = book.OrderCount;

            this.OrderCount = book.OrderCount;
            this.CustomerId = book.CustomerId;
            this.CoinId = book.CoinId;
            this.CurrencyId = book.CurrencyId;
        }

        public OrderType OrderType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Stock { get; set; }
        
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public short CoinId { get; set; }
        public virtual Coin Coin { get; set; }

        public short CurrencyId { get; set; }
        public virtual Coin Currency { get; set; }

        public Guid ContractId { get; set; }
        public virtual Contract Contract { get; set; }
        
        /// <summary>
        /// Alternate Key
        /// </summary>
        public Guid BookId  { get; set; }
        public byte OrderCount { get; set; }
    }
}
