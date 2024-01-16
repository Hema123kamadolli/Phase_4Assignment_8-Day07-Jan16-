using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment_08.Models
{
    public partial class BookStoredbContext : DbContext
    {
        public BookStoredbContext()
        {
        }

        public BookStoredbContext(DbContextOptions<BookStoredbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookCategory> BookCategories { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:booksdb08.database.windows.net,1433;Initial Catalog=BookStoredb;Persist Security Info=False;User ID=HemaAdmin08;Password=Hema@456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).ValueGeneratedNever();

                entity.Property(e => e.AuthorName).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Book__CategoryId__628FA481");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK__Book__PublisherI__6383C8BA");

                entity.HasMany(d => d.Authors)
                    .WithMany(p => p.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookAuthor",
                        l => l.HasOne<Author>().WithMany().HasForeignKey("AuthorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookAutho__Autho__6754599E"),
                        r => r.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__BookAutho__BookI__66603565"),
                        j =>
                        {
                            j.HasKey("BookId", "AuthorId").HasName("PK__BookAuth__6AED6DC43B22608E");

                            j.ToTable("BookAuthor");
                        });
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__BookCate__19093A0BF3FC8EF2");

                entity.ToTable("BookCategory");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.PublisherId).ValueGeneratedNever();

                entity.Property(e => e.PublisherAddress).HasMaxLength(50);

                entity.Property(e => e.PublisherName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
