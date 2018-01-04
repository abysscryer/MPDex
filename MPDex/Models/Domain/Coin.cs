using MPDex.Models.Base;
using System.Collections.Generic;

namespace MPDex.Models
{
    /// <summary>
    /// Coin entity
    /// </summary>
    public class Coin : BaseEntity<short>
    {
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
