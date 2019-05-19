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
                    BookId = 1,
                    Progress = 0,
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                });

            modelBuilder.Entity<UserBook>()
                .HasData(new UserBook
                {
                    BookId = 1,
                    Progress = 0,
                    UserIdentifier = "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f"
                });

            modelBuilder.Entity<UserBook>()
                .HasData(new UserBook
                {
                    BookId = 2,
                    Progress = 0,
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                });

            modelBuilder.Entity<UserBook>()
                .HasData(new UserBook
                {
                    BookId = 3,
                    Progress = 0,
                    UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                });

            return modelBuilder;
        }
    }
}
