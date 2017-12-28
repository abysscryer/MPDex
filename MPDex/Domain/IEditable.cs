using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
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
