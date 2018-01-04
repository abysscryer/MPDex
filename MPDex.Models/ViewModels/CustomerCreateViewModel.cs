using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Models.ViewModels
{
    public class CustomerCreateViewModel
    {
        public Guid Id { get; set; }
        public string NiceName { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }
    }
}
