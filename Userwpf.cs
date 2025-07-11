using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class Userwpf
    {
        public Userwpf()
        {
            Carts = new HashSet<Cart>();
            Orderbooks = new HashSet<Orderbook>();
            Reviews = new HashSet<Review>();
        }

        public int IdUser { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public DateOnly? CreatedAt { get; set; }
        public string? Name { get; set; }

        public string? NumberPhone { get; set; }
        public int? IdRole { get; set; }

        public virtual Role? IdRoleNavigation { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Orderbook> Orderbooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
