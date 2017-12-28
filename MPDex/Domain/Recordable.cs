using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    public class Recordable<T>: Entity<T>, IRecordable
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
