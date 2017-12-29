using MPDex.Models.Base;
using System.Collections.Generic;

namespace MPDex.Models
{
    public class Coin : Entity<CoinType>
    {
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
