using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orderbooks = new HashSet<Orderbook>();
        }

        public int IdStatus { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Orderbook> Orderbooks { get; set; }
    }
}
