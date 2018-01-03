using MPDex.Models.Base;
using System.Collections.Generic;

namespace MPDex.Models
{
    public class Coin : BaseEntity<byte>
    {
        public string Name { get; set; }

        // forign key constraints
        public virtual ICollection<Book> Books { get; set; }
    }
}
