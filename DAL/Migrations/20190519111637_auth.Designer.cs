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
    [Migration("20190519111637_auth")]
    partial class auth
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
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Extension")
                        .IsRequired();

                    b.Property<int?>("GenreId");

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
                            BookId = 1,
                            Author = "J. R. R. Tolkien",
                            Deleted = false,
                            Extension = "pdf",
                            GenreId = 1,
                            LastUpdated = new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(4559),
                            PageNumber = 248,
                            Title = "The Fellowship of the Ring",
                            UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            BookId = 2,
                            Author = "J. R. R. Tolkien",
                            Deleted = false,
                            Extension = "pdf",
                            GenreId = 1,
                            LastUpdated = new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(7273),
                            PageNumber = 249,
                            Title = "The Two Towers",
                            UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            BookId = 3,
                            Author = "J. R. R. Tolkien",
                            Deleted = false,
                            Extension = "pdf",
                            GenreId = 1,
                            LastUpdated = new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(7313),
                            PageNumber = 250,
                            Title = "The Return of the King",
                            UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        },
                        new
                        {
                            BookId = 4,
                            Author = "Alexander Freed",
                            Deleted = false,
                            Extension = "pdf",
                            GenreId = 2,
                            LastUpdated = new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(7329),
                            PageNumber = 195,
                            Title = "Rogue One: A Star Wars Story",
                            UploaderIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba"
                        });
                });

            modelBuilder.Entity("DAL.Entity.Bookmark", b =>
                {
                    b.Property<int>("BookmarkId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("LastUpdated");

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
                            LastUpdated = new DateTime(2019, 5, 19, 13, 16, 37, 632, DateTimeKind.Local).AddTicks(557),
                            PageNumber = 25,
                            UserId = 1
                        },
                        new
                        {
                            BookmarkId = 2,
                            BookId = 2,
                            Deleted = false,
                            LastUpdated = new DateTime(2019, 5, 19, 13, 16, 37, 632, DateTimeKind.Local).AddTicks(2057),
                            PageNumber = 37,
                            UserId = 1
                        },
                        new
                        {
                            BookmarkId = 3,
                            BookId = 3,
                            Deleted = false,
                            LastUpdated = new DateTime(2019, 5, 19, 13, 16, 37, 632, DateTimeKind.Local).AddTicks(2088),
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

                    b.Property<DateTime>("LastUpdated");

                    b.HasKey("GenreId");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Deleted = false,
                            GenreName = "Fantasy",
                            LastUpdated = new DateTime(2019, 5, 19, 13, 16, 37, 629, DateTimeKind.Local).AddTicks(5069)
                        },
                        new
                        {
                            GenreId = 2,
                            Deleted = false,
                            GenreName = "Sci-Fi",
                            LastUpdated = new DateTime(2019, 5, 19, 13, 16, 37, 630, DateTimeKind.Local).AddTicks(9288)
                        });
                });

            modelBuilder.Entity("DAL.Entity.UserBook", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<string>("UserIdentifier");

                    b.Property<bool>("Deleted");

                    b.Property<int>("Progress");

                    b.HasKey("BookId", "UserIdentifier");

                    b.ToTable("UserBook");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                            Deleted = false,
                            Progress = 0
                        },
                        new
                        {
                            BookId = 1,
                            UserIdentifier = "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f",
                            Deleted = false,
                            Progress = 0
                        },
                        new
                        {
                            BookId = 2,
                            UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                            Deleted = false,
                            Progress = 0
                        },
                        new
                        {
                            BookId = 3,
                            UserIdentifier = "87d92da2-13df-47d5-85d7-b3f0fc3d99ba",
                            Deleted = false,
                            Progress = 0
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