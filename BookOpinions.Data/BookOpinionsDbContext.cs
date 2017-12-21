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

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Rating> Ratings{ get; set; }
        public DbSet<Image> Images{ get; set; }

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

            builder
                .Entity<Opinion>()
                .HasOne(o => o.Book)
                .WithMany(b => b.Opinions)
                .HasForeignKey(o => o.BookId);

            builder
                .Entity<Opinion>()
                .HasOne(o => o.User)
                .WithMany(u => u.Opinions)
                .HasForeignKey(o => o.UserId);

            builder
                .Entity<Rating>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Rating)
                .HasForeignKey(r => r.BookId);

            builder
                .Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);

            builder
                .Entity<Book>()
                .HasOne(b => b.Image)
                .WithMany(i => i.Books)
                .HasForeignKey(b => b.ImageId);

            base.OnModelCreating(builder);
        }
    }
}
