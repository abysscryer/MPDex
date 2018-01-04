using MPDex.Models.Base;
using System;
using System.Collections.Generic;

namespace MPDex.Models
{
    /// <summary>
    /// Customer entity
    /// </summary>
    public class Customer : Editable<Guid>
    {
        public string NiceName { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        
        public virtual ICollection<Book> Books { get; set; }
    }
}