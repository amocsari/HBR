﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(HbrDbContext))]
    [Migration("20190520041040_ubid")]
    partial class ubid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Entity.Book", b =>
                {
                    b.Property<string>("BookId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Extension")
                        .IsRequired();

                    b.Property<string>("GenreId");

                    b.Property<string>("Isbn");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("PageNumber");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("UploaderIdentifier")
                        .IsRequired();

                    b.HasKey("BookId");

                    b.HasIndex("GenreId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            BookId = "466a182c-dd53-4c27-affb-75fb4ff6e220",
                            Author = "J. R. R. Tolkien",
                            Deleted = false,
                            Extension = "pdf",
                            GenreId = "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                            LastUpdated = new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(5087),
                            PageNumber = 248,
                            Title = "The Fellowship of the Ring",
                            UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            BookId = "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                            Author = "J. R. R. Tolkien",
                            Deleted = false,
                            Extension = "pdf",
                            GenreId = "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                            LastUpdated = new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(7718),
                            PageNumber = 249,
                            Title = "The Two Towers",
                            UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            BookId = "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                            Author = "J. R. R. Tolkien",
                            Deleted = false,
                            Extension = "pdf",
                            GenreId = "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                            LastUpdated = new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(7755),
                            PageNumber = 250,
                            Title = "The Return of the King",
                            UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            BookId = "1a07dc8b-8ef3-4757-a4f2-bdef6b78bd3a",
                            Author = "Alexander Freed",
                            Deleted = false,
                            Extension = "pdf",
                            GenreId = "7a4f9a69-3afd-4dae-829a-ae4691a415db",
                            LastUpdated = new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(7771),
                            PageNumber = 195,
                            Title = "Rogue One: A Star Wars Story",
                            UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        });
                });

            modelBuilder.Entity("DAL.Entity.Bookmark", b =>
                {
                    b.Property<string>("BookmarkId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BookId")
                        .IsRequired();

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("PageNumber");

                    b.Property<string>("UserIdentifier")
                        .IsRequired();

                    b.HasKey("BookmarkId");

                    b.HasIndex("BookId");

                    b.ToTable("Bookmark");

                    b.HasData(
                        new
                        {
                            BookmarkId = "ad536b7d-08df-48fe-87e4-7253c1c76eac",
                            BookId = "466a182c-dd53-4c27-affb-75fb4ff6e220",
                            Deleted = false,
                            LastUpdated = new DateTime(2019, 5, 20, 6, 10, 40, 357, DateTimeKind.Local).AddTicks(1058),
                            PageNumber = 25,
                            UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            BookmarkId = "bbccd264-fb13-421d-8335-c3267bdeaeea",
                            BookId = "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                            Deleted = false,
                            LastUpdated = new DateTime(2019, 5, 20, 6, 10, 40, 357, DateTimeKind.Local).AddTicks(2591),
                            PageNumber = 37,
                            UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            BookmarkId = "74762732-bc6b-4346-a60a-9117902afbd9",
                            BookId = "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                            Deleted = false,
                            LastUpdated = new DateTime(2019, 5, 20, 6, 10, 40, 357, DateTimeKind.Local).AddTicks(2621),
                            PageNumber = 48,
                            UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        });
                });

            modelBuilder.Entity("DAL.Entity.Genre", b =>
                {
                    b.Property<string>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<string>("GenreName")
                        .IsRequired();

                    b.Property<DateTime>("LastUpdated");

                    b.HasKey("GenreId");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            GenreId = "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                            Deleted = false,
                            GenreName = "Fantasy",
                            LastUpdated = new DateTime(2019, 5, 20, 6, 10, 40, 354, DateTimeKind.Local).AddTicks(5304)
                        },
                        new
                        {
                            GenreId = "7a4f9a69-3afd-4dae-829a-ae4691a415db",
                            Deleted = false,
                            GenreName = "Sci-Fi",
                            LastUpdated = new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(191)
                        });
                });

            modelBuilder.Entity("DAL.Entity.User", b =>
                {
                    b.Property<string>("UserName");

                    b.Property<string>("Password");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("UserName", "Password");

                    b.HasAlternateKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserName = "amocsari",
                            Password = "12Wasd34",
                            UserId = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            UserName = "teszt",
                            Password = "12Wasd34",
                            UserId = "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f"
                        });
                });

            modelBuilder.Entity("DAL.Entity.UserBook", b =>
                {
                    b.Property<int>("UserBookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookId")
                        .IsRequired();

                    b.Property<bool>("Deleted");

                    b.Property<int>("Progress");

                    b.Property<string>("UserIdentifier")
                        .IsRequired();

                    b.HasKey("UserBookId");

                    b.HasIndex("BookId");

                    b.ToTable("UserBook");

                    b.HasData(
                        new
                        {
                            UserBookId = 1,
                            BookId = "466a182c-dd53-4c27-affb-75fb4ff6e220",
                            Deleted = false,
                            Progress = 0,
                            UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            UserBookId = 2,
                            BookId = "466a182c-dd53-4c27-affb-75fb4ff6e220",
                            Deleted = false,
                            Progress = 0,
                            UserIdentifier = "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f"
                        },
                        new
                        {
                            UserBookId = 3,
                            BookId = "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                            Deleted = false,
                            Progress = 0,
                            UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            UserBookId = 4,
                            BookId = "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                            Deleted = false,
                            Progress = 0,
                            UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
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

            modelBuilder.Entity("DAL.Entity.UserBook", b =>
                {
                    b.HasOne("DAL.Entity.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
