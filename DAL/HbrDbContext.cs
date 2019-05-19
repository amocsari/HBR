using DAL.Entity;
using DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class HbrDbContext : DbContext, IHbrDbContext
    {
        public HbrDbContext(DbContextOptions<HbrDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasQueryFilter(book => book.Deleted == false);
            modelBuilder.Entity<Genre>()
                .HasQueryFilter(book => book.Deleted == false);
            modelBuilder.Entity<UserBook>()
                .HasQueryFilter(book => book.Deleted == false);
            modelBuilder.Entity<Bookmark>()
                .HasQueryFilter(book => book.Deleted == false);

            modelBuilder.Entity<UserBook>()
                .HasKey(c => new { c.BookId, c.UserIdentifier });

            modelBuilder
                .SeedGenres()
                .SeedBooks()
                .SeedBookmarks()
                .SeedUserBooks();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
    }
}
