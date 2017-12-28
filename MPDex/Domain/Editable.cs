using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    public class Editable<T>: Entity<T>, IEditable
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
