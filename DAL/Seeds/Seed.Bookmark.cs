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
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                    PageNumber = 25
                });

            modelBuilder.Entity<Bookmark>()
                .HasData(new Bookmark
                {
                    BookmarkId = 2,
                    BookId = 2,
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                    PageNumber = 37
                });

            modelBuilder.Entity<Bookmark>()
                .HasData(new Bookmark
                {
                    BookmarkId = 3,
                    BookId = 3,
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                    PageNumber = 48
                });

            return modelBuilder;
        }
    }
}
