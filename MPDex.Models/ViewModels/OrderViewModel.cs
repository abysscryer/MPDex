using MPDex.Models.Base;

namespace MPDex.Models.ViewModels
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public OrderType OrderType { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Stock { get; set; }
        
        //public Guid CustomerId { get; set; }
        public virtual string NickName { get; set; }

        //public short CoinId { get; set; }
        public virtual string CoinName { get; set; }

        //public long ContractId { get; set; }
        public virtual long ContractId { get; set; }
    }
}
