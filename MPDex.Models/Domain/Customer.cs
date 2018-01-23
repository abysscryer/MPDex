using MPDex.Models.Base;
using System;
using System.Collections.Generic;

namespace MPDex.Models.Domain
{
    /// <summary>
    /// 고객
    /// </summary>
    public class Customer : Creatable<Guid>
    {
        public string NickName { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }

        public virtual ICollection<Balance> Balances { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
        public virtual ICollection<Statement> Statements { get; set; }
    }
}