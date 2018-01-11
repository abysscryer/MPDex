using MPDex.Models.Base;

namespace MPDex.Models.ViewModels
{
    public class BookViewModel
    {
        public OrderType BookType { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public decimal Stock { get; set; }

        public string NickName { get; set; }

        public string CoinName { get; set; }
    }
}