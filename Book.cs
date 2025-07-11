using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections.ObjectModel;

namespace EkatBooks
{
    public partial class Book
    {
        public Book()
        {
            BookTrends = new HashSet<BookTrend>();
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
            Reviews = new HashSet<Review>();

           
           

        }

        public int IdBook { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public DateOnly? PublicationDate { get; set; }
        public string? Isbn { get; set; }
        public string? Description { get; set; }
        public string? CoverImage { get; set; }
        public int? QuantityBooks { get; set; }
        public int? IdAuthor { get; set; }
        public int? IdCategory { get; set; }

        public virtual Author? IdAuthorNavigation { get; set; }
        public virtual Category? IdCategoryNavigation { get; set; }
        public virtual ICollection<BookTrend> BookTrends { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
