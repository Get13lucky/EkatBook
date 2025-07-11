using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EkatBooks
{
    public partial class BooksContext : DbContext
    {
        public BooksContext()
        {
        }

        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookTrend> BookTrends { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<Orderbook> Orderbooks { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Trend> Trends { get; set; } = null!;
        public virtual DbSet<Userwpf> Userwpfs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Books;Username=postgres;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.IdAuthor)
                    .HasName("author_pkey");

                entity.ToTable("author");

                entity.Property(e => e.IdAuthor)
                    .ValueGeneratedNever()
                    .HasColumnName("id_author");

                entity.Property(e => e.Bio)
                    .HasMaxLength(2000)
                    .HasColumnName("bio");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .HasColumnName("photo");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.IdBook)
                    .HasName("book_pkey");

                entity.ToTable("book");

                entity.HasIndex(e => e.Isbn, "book_isbn_key")
                    .IsUnique();

                entity.Property(e => e.IdBook)
                    .ValueGeneratedNever()
                    .HasColumnName("id_book");

                entity.Property(e => e.CoverImage)
                    .HasMaxLength(255)
                    .HasColumnName("cover_image");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .HasColumnName("description");

                entity.Property(e => e.IdAuthor).HasColumnName("id_author");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(13)
                    .HasColumnName("isbn");

                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasColumnName("price");

                entity.Property(e => e.PublicationDate).HasColumnName("publication_date");

                entity.Property(e => e.QuantityBooks)
                    .HasColumnName("quantity_books")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IdAuthor)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("book_id_author_fkey");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("book_id_category_fkey");
            });

            modelBuilder.Entity<BookTrend>(entity =>
            {
                entity.HasKey(e => e.IdBookTrend)
       .HasName("book_trend_pkey");

                entity.ToTable("book_trend");

                entity.Property(e => e.IdBookTrend)
                    .HasColumnName("id_book_trend")
                    .UseIdentityAlwaysColumn() // Добавляем автоинкремент
                    .IsRequired();

                entity.Property(e => e.IdBook).HasColumnName("id_book");
                entity.Property(e => e.IdTrend).HasColumnName("id_trend");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.BookTrends)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("book_trend_id_book_fkey");

                entity.HasOne(d => d.IdTrendNavigation)
                    .WithMany(p => p.BookTrends)
                    .HasForeignKey(d => d.IdTrend)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("book_trend_id_trend_fkey");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.IdCart)
                    .HasName("cart_pkey");

                entity.ToTable("cart");

                entity.Property(e => e.IdCart)
                    .ValueGeneratedNever()
                    .HasColumnName("id_cart");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("cart_id_user_fkey");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.IdCartElements)
                    .HasName("cart_item_pkey");

                entity.ToTable("cart_item");

                entity.Property(e => e.IdCartElements)
                    .ValueGeneratedNever()
                    .HasColumnName("id_cart_elements");

                entity.Property(e => e.IdBook).HasColumnName("id_book");

                entity.Property(e => e.IdCart).HasColumnName("id_cart");

                entity.Property(e => e.QuantityGoods).HasColumnName("quantity_goods");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("cart_item_id_book_fkey");

                entity.HasOne(d => d.IdCartNavigation)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.IdCart)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("cart_item_id_cart_fkey");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("category_pkey");

                entity.ToTable("category");

                entity.Property(e => e.IdCategory)
                    .ValueGeneratedNever()
                    .HasColumnName("id_category");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.IdOrderItem)
                    .HasName("order_item_pkey");

                entity.ToTable("order_item");

                entity.Property(e => e.IdOrderItem)
                 .HasColumnName("id_order_item")
                 .UseIdentityAlwaysColumn() // Добавляем для автоинкремента в PostgreSQL
                 .IsRequired();

                entity.Property(e => e.IdBook).HasColumnName("id_book");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.QuantityGoodsUnique).HasColumnName("quantity_goods_unique");

                entity.Property(e => e.TotalPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("total_price");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("order_item_id_book_fkey");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("order_item_id_order_fkey");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("order_status_pkey");

                entity.ToTable("order_status");

                entity.Property(e => e.IdStatus)
                    .ValueGeneratedNever()
                    .HasColumnName("id_status");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                // Добавляем начальные данные
                entity.HasData(
                    new OrderStatus { IdStatus = 1, Status = "В обработке" },
                    new OrderStatus { IdStatus = 2, Status = "Оплачен" },
                    new OrderStatus { IdStatus = 3, Status = "В доставке" },
                    new OrderStatus { IdStatus = 4, Status = "Доставлен" },
                    new OrderStatus { IdStatus = 5, Status = "Отменен" }
                );
            });

            modelBuilder.Entity<Orderbook>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("orderbook_pkey");

                entity.ToTable("orderbook");

                entity.Property(e => e.IdOrder)
                    .HasColumnName("id_order")
                    .UseIdentityAlwaysColumn() // Add this line
                    .IsRequired();

                entity.Property(e => e.DeliveryMethod)
    .HasMaxLength(50)
    .HasColumnName("delivery_method");

                entity.Property(e => e.DeliveryAddress)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_address");

                entity.Property(e => e.IdPayment).HasColumnName("id_payment");

                entity.Property(e => e.IdStatus).HasColumnName("id_status");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.OrderDate).HasColumnName("order_date");

                entity.Property(e => e.TotalPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("total_price");

                entity.HasOne(d => d.IdPaymentNavigation)
                    .WithMany(p => p.Orderbooks)
                    .HasForeignKey(d => d.IdPayment)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("orderbook_id_payment_fkey");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Orderbooks)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("orderbook_id_status_fkey");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orderbooks)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("orderbook_id_user_fkey");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.IdPayment)
                    .HasName("payment_pkey");

                entity.ToTable("payment");

                entity.Property(e => e.IdPayment)
                    .HasColumnName("id_payment")
                    .UseIdentityAlwaysColumn()  // Используем вместо HasIdentityOptions
                    .IsRequired();

                entity.Property(e => e.ChoicePayment)
                    .HasMaxLength(255)
                    .HasColumnName("choice_payment");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("payment_date");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.IdReview)
                    .HasName("review_pkey");

                entity.ToTable("review");

                entity.Property(e => e.IdReview)
                    .ValueGeneratedNever()
                    .HasColumnName("id_review");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.IdBook).HasColumnName("id_book");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReviewText)
                    .HasMaxLength(2000)
                    .HasColumnName("review_text");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("review_id_book_fkey");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("review_id_user_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("role_pkey");

                entity.ToTable("role");

                entity.Property(e => e.IdRole)
                    .ValueGeneratedNever()
                    .HasColumnName("id_role");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(255)
                    .HasColumnName("name_role");
            });

            modelBuilder.Entity<Trend>(entity =>
            {
                entity.HasKey(e => e.IdTrend)
                    .HasName("trend_pkey");

                entity.ToTable("trend");

                entity.Property(e => e.IdTrend)
                    .ValueGeneratedNever()
                    .HasColumnName("id_trend");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Userwpf>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("userwpf_pkey");

                entity.ToTable("userwpf");

                entity.HasIndex(e => e.Email, "userwpf_email_key")
                    .IsUnique();

                entity.HasIndex(e => e.Login, "userwpf_login_key")
                    .IsUnique();

                entity.HasIndex(e => e.NumberPhone, "userwpf_number_phone_key")
                    .IsUnique();

                entity.Property(e => e.IdUser)
                    .ValueGeneratedNever()
                    .HasColumnName("id_user");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.Login)
                    .HasMaxLength(25)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.NumberPhone)
                    .HasMaxLength(25)
                    .HasColumnName("number_phone");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .HasColumnName("password_hash");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Userwpfs)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("userwpf_id_role_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
