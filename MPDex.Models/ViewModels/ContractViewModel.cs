using System.Collections.Generic;

namespace MPDex.Models.ViewModels
{
    public class ContractViewModel
    {
        public long Id { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public virtual ICollection<OrderCreateModel> Orders { get; set; }
    }
}
