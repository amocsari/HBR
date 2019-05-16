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
                    BookId = 1,
                    Title = "The Fellowship of the Ring",
                    Author = "J. R. R. Tolkien",
                    PageNumber = 248,
                    GenreId = 1
                });

            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    BookId = 2,
                    Title = "The Two Towers",
                    Author = "J. R. R. Tolkien",
                    PageNumber = 249,
                    GenreId = 1
                });

            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    BookId = 3,
                    Title = "The Return of the King",
                    Author = "J. R. R. Tolkien",
                    PageNumber = 250,
                    GenreId = 1
                });

            modelBuilder.Entity<Book>()
                .HasData(new Book
                {
                    BookId = 4,
                    Title = "Rogue One: A Star Wars Story",
                    Author = "Alexander Freed",
                    PageNumber = 195,
                    GenreId = 2
                });

            return modelBuilder;
        }
    }
}
