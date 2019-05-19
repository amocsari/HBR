using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds
{
    public static partial class Seed
    {
        public static ModelBuilder SeedBooks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    BookId = "466a182c-dd53-4c27-affb-75fb4ff6e220",
                    Title = "The Fellowship of the Ring",
                    Author = "J. R. R. Tolkien",
                    PageNumber = 248,
                    GenreId = "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                    Extension = "pdf",
                    UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                });

            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    BookId = "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                    Title = "The Two Towers",
                    Author = "J. R. R. Tolkien",
                    PageNumber = 249,
                    GenreId = "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                    Extension = "pdf",
                    UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                });

            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    BookId = "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                    Title = "The Return of the King",
                    Author = "J. R. R. Tolkien",
                    PageNumber = 250,
                    GenreId = "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                    Extension = "pdf",
                    UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                });

            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    BookId = "1a07dc8b-8ef3-4757-a4f2-bdef6b78bd3a",
                    Title = "Rogue One: A Star Wars Story",
                    Author = "Alexander Freed",
                    PageNumber = 195,
                    GenreId = "7a4f9a69-3afd-4dae-829a-ae4691a415db",
                    Extension = "pdf",
                    UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                });

            return modelBuilder;
        }
    }
}
