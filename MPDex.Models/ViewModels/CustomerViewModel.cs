using MPDex.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MPDex.Models.ViewModels
{
    public class CustomerCreateModel
    {
        public Guid Id { get; set; }

        [Required, MinLength(2), MaxLength(16)]
        public string NickName { get; set; }

        [Required, MaxLength(16)]
        public string FamilyName { get; set; }

        [Required, MaxLength(16)]
        public string GivenName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Balance> Balances { get; set; }
    }

    public class CustomerUpdateModel
    {
        [Required, Phone]
        public string CellPhone { get; set; }
    }

    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
    }
}
