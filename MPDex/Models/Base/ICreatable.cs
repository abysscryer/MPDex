using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Base
{
    public interface ICreatable
    {
        DateTime OnCreated { get; set; }
    }
}
