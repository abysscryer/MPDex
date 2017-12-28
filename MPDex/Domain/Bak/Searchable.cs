using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MPDex.Domain
{
    public class Searchable
    {
        [NotMapped]
        DateTime StartAt { get; set; }
        [NotMapped]
        DateTime EndAt { get; set; }
    }
}
