using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    /// <summary>
    /// auditable model interface
    /// </summary>
    public interface IAuditable : IEditable
    {
        string IPAddress { get; set; }
        byte[] Version { get; set; }
    }
}
