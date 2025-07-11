using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class Trend
    {
        public Trend()
        {
            BookTrends = new HashSet<BookTrend>();
        }

        public int IdTrend { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<BookTrend> BookTrends { get; set; }
    }
}
