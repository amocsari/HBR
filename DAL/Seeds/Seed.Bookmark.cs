using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds
{
    public partial class Seed
    {
        public static ModelBuilder SeedBookmarks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmark>()
                .HasData(new Bookmark
                {
                    BookmarkId = 1,
                    BookId = 1,
                    UserId = 1,
                    PageNumber = 25
                });

            modelBuilder.Entity<Bookmark>()
                .HasData(new Bookmark
                {
                    BookmarkId = 2,
                    BookId = 2,
                    UserId = 1,
                    PageNumber = 37
                });

            modelBuilder.Entity<Bookmark>()
                .HasData(new Bookmark
                {
                    BookmarkId = 3,
                    BookId = 3,
                    UserId = 1,
                    PageNumber = 48
                });

            return modelBuilder;
        }
    }
}
