using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    public interface IRecordable
    {
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }
    }
}
