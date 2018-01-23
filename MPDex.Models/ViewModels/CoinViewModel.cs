using System;
using System.ComponentModel.DataAnnotations;

namespace MPDex.Models.ViewModels
{
    public class CoinCreateModel
    {
        public short Id { get; set; }

        [Required, MinLength(2), MaxLength(16)]
        public string Name { get; set; }
    }

    public class CoinViewModel
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public DateTime OnCreated { get; set; }
    }
}
