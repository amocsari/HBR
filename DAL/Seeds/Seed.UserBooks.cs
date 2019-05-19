using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds
{
    public partial class Seed
    {
        public static ModelBuilder SeedUserBooks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBook>()
                .HasData(new UserBook
                {
                    BookId = "466a182c-dd53-4c27-affb-75fb4ff6e220",
                    Progress = 0,
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                });

            modelBuilder.Entity<UserBook>()
                .HasData(new UserBook
                {
                    BookId = "466a182c-dd53-4c27-affb-75fb4ff6e220",
                    Progress = 0,
                    UserIdentifier = "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f"
                });

            modelBuilder.Entity<UserBook>()
                .HasData(new UserBook
                {
                    BookId = "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                    Progress = 0,
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                });

            modelBuilder.Entity<UserBook>()
                .HasData(new UserBook
                {
                    BookId = "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                    Progress = 0,
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                });

            return modelBuilder;
        }
    }
}
