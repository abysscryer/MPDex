using System;

namespace MPDex.Models.Base
{
    /// <summary>
    /// editable model abstract class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Editable<T>: BaseEntity<T>, IEditable
    {
        public DateTime OnCreated { get; set; }
        public DateTime? OnUpdated { get; set; }
    }
}
