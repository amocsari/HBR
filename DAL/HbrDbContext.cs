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

            #region tmp ki lesz veve
            modelBuilder.Entity<User>()
                .HasKey(c => new { c.UserName, c.Password });
            #endregion

            modelBuilder
                .SeedGenres()
                .SeedBooks()
                .SeedBookmarks()
                .SeedUserBooks();
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Bookmark> Bookmark { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        #region tmp ki lesz veve
        public DbSet<User> User { get; set; }
        #endregion
    }
}
