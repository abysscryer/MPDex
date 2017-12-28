using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    /// <summary>
    /// editable model abstract class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Editable<T>: Entity<T>, IEditable
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
