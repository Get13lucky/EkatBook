using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int IdCart { get; set; }
        public int? IdUser { get; set; }

        public virtual Userwpf? IdUserNavigation { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
