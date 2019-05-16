﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(HbrDbContext))]
    partial class HbrDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Entity.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<bool>("Deleted");

                    b.Property<int?>("GenreId");

                    b.Property<string>("Isbn");

                    b.Property<int>("PageNumber");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("BookId");

                    b.HasIndex("GenreId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Author = "J. R. R. Tolkien",
                            Deleted = false,
                            GenreId = 1,
                            PageNumber = 248,
                            Title = "The Fellowship of the Ring"
                        },
                        new
                        {
                            BookId = 2,
                            Author = "J. R. R. Tolkien",
                            Deleted = false,
                            GenreId = 1,
                            PageNumber = 249,
                            Title = "The Two Towers"
                        },
                        new
                        {
                            BookId = 3,
                            Author = "J. R. R. Tolkien",
                            Deleted = false,
                            GenreId = 1,
                            PageNumber = 250,
                            Title = "The Return of the King"
                        },
                        new
                        {
                            BookId = 4,
                            Author = "Alexander Freed",
                            Deleted = false,
                            GenreId = 2,
                            PageNumber = 195,
                            Title = "Rogue One: A Star Wars Story"
                        });
                });

            modelBuilder.Entity("DAL.Entity.Bookmark", b =>
                {
                    b.Property<int>("BookmarkId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<bool>("Deleted");

                    b.Property<int>("PageNumber");

                    b.Property<int>("UserId");

                    b.HasKey("BookmarkId");

                    b.HasIndex("BookId");

                    b.ToTable("Bookmark");

                    b.HasData(
                        new
                        {
                            BookmarkId = 1,
                            BookId = 1,
                            Deleted = false,
                            PageNumber = 25,
                            UserId = 1
                        },
                        new
                        {
                            BookmarkId = 2,
                            BookId = 2,
                            Deleted = false,
                            PageNumber = 37,
                            UserId = 1
                        },
                        new
                        {
                            BookmarkId = 3,
                            BookId = 3,
                            Deleted = false,
                            PageNumber = 48,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("DAL.Entity.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<string>("GenreName")
                        .IsRequired();

                    b.HasKey("GenreId");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Deleted = false,
                            GenreName = "Fantasy"
                        },
                        new
                        {
                            GenreId = 2,
                            Deleted = false,
                            GenreName = "Sci-Fi"
                        });
                });

            modelBuilder.Entity("DAL.Entity.UserBook", b =>
                {
                    b.Property<int>("UserBookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<bool>("Deleted");

                    b.Property<int>("Progress");

                    b.Property<int>("UserId");

                    b.HasKey("UserBookId");

                    b.ToTable("UserBook");

                    b.HasData(
                        new
                        {
                            UserBookId = 1,
                            BookId = 1,
                            Deleted = false,
                            Progress = 0,
                            UserId = 1
                        },
                        new
                        {
                            UserBookId = 2,
                            BookId = 2,
                            Deleted = false,
                            Progress = 0,
                            UserId = 1
                        },
                        new
                        {
                            UserBookId = 3,
                            BookId = 3,
                            Deleted = false,
                            Progress = 0,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("DAL.Entity.Book", b =>
                {
                    b.HasOne("DAL.Entity.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("DAL.Entity.Bookmark", b =>
                {
                    b.HasOne("DAL.Entity.Book")
                        .WithMany("Bookmarks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
