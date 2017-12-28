using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    public interface IAuditable : IEditable
    {
        string IPAddress { get; set; }
        byte[] Version { get; set; }
    }
}
