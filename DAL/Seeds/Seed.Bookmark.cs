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
                    BookmarkId = "ad536b7d-08df-48fe-87e4-7253c1c76eac",
                    BookId = "466a182c-dd53-4c27-affb-75fb4ff6e220",
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                    PageNumber = 25
                });

            modelBuilder.Entity<Bookmark>()
                .HasData(new Bookmark
                {
                    BookmarkId = "bbccd264-fb13-421d-8335-c3267bdeaeea",
                    BookId = "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                    PageNumber = 37
                });

            modelBuilder.Entity<Bookmark>()
                .HasData(new Bookmark
                {
                    BookmarkId = "74762732-bc6b-4346-a60a-9117902afbd9",
                    BookId = "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                    PageNumber = 48
                });

            return modelBuilder;
        }
    }
}
