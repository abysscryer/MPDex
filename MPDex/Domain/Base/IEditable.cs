using System;

namespace MPDex.Domain.Base
{
    /// <summary>
    /// editable model interface
    /// </summary>
    public interface IEditable
    {
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }
    }
}
