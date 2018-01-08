using System;

namespace MPDex.Models.Base
{
    /// <summary>
    /// editable model abstract class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Editable<T>: Creatable<T>, IEditable
    {
        public DateTime? OnUpdated { get; set; }
        public byte[] RowVersion { get; set; }
        
    }
}
