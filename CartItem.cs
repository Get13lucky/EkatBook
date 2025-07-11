using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class CartItem
    {
        public int IdCartElements { get; set; }
        public int? IdCart { get; set; }
        public int? IdBook { get; set; }
        public int? QuantityGoods { get; set; }

        public virtual Book? IdBookNavigation { get; set; }
        public virtual Cart? IdCartNavigation { get; set; }
    }
}
