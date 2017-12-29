using MPDex.Models.Base;
using System;
using System.Collections.Generic;

namespace MPDex.Models
{
    public class Customer : Editable<Guid>
    {
        public Customer()
        {}

        public Customer(string nickName, string familyName, string givenName, string email, string cellPhone)
        {
            this.Id = Guid.NewGuid();
            this.NiceName = nickName;
            this.FamilyName = familyName;
            this.GivenName = givenName;
            this.Email = email;
            this.CellPhone = cellPhone;
        }

        public string NiceName { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}