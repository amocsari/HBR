using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds
{
    public partial class Seed
    {
        public static ModelBuilder SeedGenres(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasData(new Genre
                {
                    GenreId = 1,
                    GenreName = "Fantasy"
                });

            modelBuilder.Entity<Genre>()
                .HasData(new Genre
                {
                    GenreId = 2,
                    GenreName = "Sci-Fi"
                });

            return modelBuilder;
        }
    }
}
