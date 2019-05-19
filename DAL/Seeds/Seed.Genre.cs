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
                    GenreId = "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                    GenreName = "Fantasy"
                });

            modelBuilder.Entity<Genre>()
                .HasData(new Genre
                {
                    GenreId = "7a4f9a69-3afd-4dae-829a-ae4691a415db",
                    GenreName = "Sci-Fi"
                });

            return modelBuilder;
        }
    }
}
