using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class Orderbook
    {
        public Orderbook()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int IdOrder { get; set; }
        public string DeliveryAddress { get; set; }
        public int? IdPayment { get; set; }
        public int? IdStatus { get; set; }
        public int? IdUser { get; set; }
        public DateOnly? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }

        public string? DeliveryMethod { get; set; }

        public virtual Payment IdPaymentNavigation { get; set; }
        public virtual OrderStatus IdStatusNavigation { get; set; }
        public virtual Userwpf IdUserNavigation { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
