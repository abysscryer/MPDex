using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.Base
{
    public class Creatable<T> : Entity<T>, ICreatable
    {
        public DateTime OnCreated { get; set; }
    }
}
