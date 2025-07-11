using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class BookTrend
    {
        public int IdBookTrend { get; set; }
        public int? IdBook { get; set; }
        public int? IdTrend { get; set; }

        public virtual Book? IdBookNavigation { get; set; }
        public virtual Trend? IdTrendNavigation { get; set; }
    }
}
