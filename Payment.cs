using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class Payment
    {
        public Payment()
        {
            Orderbooks = new HashSet<Orderbook>();
        }

        public int IdPayment { get; set; }
        public string? ChoicePayment { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual ICollection<Orderbook> Orderbooks { get; set; }
    }
}
