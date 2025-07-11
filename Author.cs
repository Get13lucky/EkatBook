using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int IdAuthor { get; set; }
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string? Photo { get; set; }
        public DateOnly? BirthDate { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
