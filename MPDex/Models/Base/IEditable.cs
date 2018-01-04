using System;

namespace MPDex.Models.Base
{
    /// <summary>
    /// editable model interface
    /// </summary>
    public interface IEditable
    {
        DateTime OnCreated { get; set; }
        DateTime? OnUpdated { get; set; }
    }
}
