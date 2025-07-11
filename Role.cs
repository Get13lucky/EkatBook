using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class Role
    {
        public Role()
        {
            Userwpfs = new HashSet<Userwpf>();
        }

        public int IdRole { get; set; }
        public string? NameRole { get; set; }

        public virtual ICollection<Userwpf> Userwpfs { get; set; }
    }
}
