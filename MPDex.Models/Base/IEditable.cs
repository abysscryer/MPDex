using System;

namespace MPDex.Models.Base
{
    /// <summary>
    /// editable model interface
    /// </summary>
    public interface IEditable : ICreatable
    {
        DateTime? OnUpdated { get; set; }
        byte[] RowVersion { get; set; }
    }
}
