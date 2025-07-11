using System;
using System.Collections.Generic;

namespace EkatBooks
{
    public partial class Review
    {
        public int IdReview { get; set; }
        public int? Rating { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? IdBook { get; set; }
        public int? IdUser { get; set; }

        public virtual Book? IdBookNavigation { get; set; }
        public virtual Userwpf? IdUserNavigation { get; set; }
    }
}
