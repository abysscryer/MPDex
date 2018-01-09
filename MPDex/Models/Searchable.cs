using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPDex.Domain.Base
{
    public class Searchable
    {
        [NotMapped]
        DateTime StartAt { get; set; }
        [NotMapped]
        DateTime EndAt { get; set; }
    }
}
