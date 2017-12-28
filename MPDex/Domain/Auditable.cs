using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    public class Auditable<T> : Recordable<T>, IAuditable
    {
        public Customer CreatedBy { get; set; }
        public Customer UpdatedBy { get; set; }
        public string IPAddress { get; set; }
        public byte[] Version { get; set; }
        
    }
}
