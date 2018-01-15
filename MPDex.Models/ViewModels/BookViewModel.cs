using MPDex.Models.Base;
using System;

namespace MPDex.Models.ViewModels
{
    public class BookViewModel
    {
        public Guid Id { get; set; }

        public OrderType OrderType { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public decimal Stock { get; set; }
        
        public Guid CustomerId { get; set; }
        
        public string NickName { get; set; }

        public short CoinId { get; set; }

        public string CoinName { get; set; }
    }
}