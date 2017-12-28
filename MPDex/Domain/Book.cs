using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    public class Book : Auditable<Guid>
    {
        public Book()
        { }

        public Book(BookType bookType, decimal price, decimal amount, decimal stock, Customer customer, Coin coin)
        {
            this.Id = Guid.NewGuid();
            this.BookType = bookType;
            this.Price = price;
            this.Amount = amount;
            this.Stock = stock;
            this.Customer = customer;
            this.Coin = coin;
        }
        
        public BookType BookType { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public decimal Stock { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Coin Coin { get; set; }
        
    }
}

