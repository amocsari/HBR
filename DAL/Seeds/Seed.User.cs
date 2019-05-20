using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds
{
    public partial class Seed
    {
        public static ModelBuilder SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    UserId = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                    UserName = "amocsari",
                    Password = "12Wasd34"
                });

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    UserId = "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f",
                    UserName = "teszt",
                    Password = "12Wasd34"
                });

            return modelBuilder;
        }
    }
}
