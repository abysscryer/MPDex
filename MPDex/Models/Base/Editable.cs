using System;

namespace MPDex.Models.Base
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
