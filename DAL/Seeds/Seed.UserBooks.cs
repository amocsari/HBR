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
                    UserBookId = 1,
                    BookId = 1,
                    Progress = 0,
                    UserId = 1
                });

            modelBuilder.Entity<UserBook>()
                .HasData(new UserBook
                {
                    UserBookId = 2,
                    BookId = 2,
                    Progress = 0,
                    UserId = 1
                });

            modelBuilder.Entity<UserBook>()
                .HasData(new UserBook
                {
                    UserBookId = 3,
                    BookId = 3,
                    Progress = 0,
                    UserId = 1
                });

            return modelBuilder;
        }
    }
}
