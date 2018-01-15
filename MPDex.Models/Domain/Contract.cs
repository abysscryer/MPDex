using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 계약
    /// </summary>
    public class Contract : Entity<Guid>
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        public virtual Trade Trade { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
