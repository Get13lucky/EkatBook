using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class OrderItem
    {
        public int IdOrderItem { get; set; }
        public int? IdOrder { get; set; }
        public int? IdBook { get; set; }
        public int? QuantityGoodsUnique { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual Book? IdBookNavigation { get; set; }
        public virtual Orderbook? IdOrderNavigation { get; set; }
    }
}
