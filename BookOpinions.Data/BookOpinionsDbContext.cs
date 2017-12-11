using BookOpinions.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookOpinions.Data
{
    public class BookOpinionsDbContext : IdentityDbContext<User>
    {
        public BookOpinionsDbContext() : base() { }

        public BookOpinionsDbContext(DbContextOptions<BookOpinionsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Opinion> Opinions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<BookAuthor>()
                .HasKey(b => new { b.AuthorId, b.BookId });

            builder
                .Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(ba => ba.BookId);

            builder
                .Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(ba => ba.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
