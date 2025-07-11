using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int IdCategory { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
