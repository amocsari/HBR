using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<string>(nullable: false),
                    GenreName = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<string>(nullable: false),
                    Isbn = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    PageNumber = table.Column<int>(nullable: false),
                    GenreId = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Extension = table.Column<string>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false),
                    UploaderIdentifier = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookmark",
                columns: table => new
                {
                    BookmarkId = table.Column<string>(nullable: false),
                    BookId = table.Column<string>(nullable: false),
                    UserIdentifier = table.Column<string>(nullable: false),
                    PageNumber = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmark", x => x.BookmarkId);
                    table.ForeignKey(
                        name: "FK_Bookmark_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBook",
                columns: table => new
                {
                    BookId = table.Column<string>(nullable: false),
                    UserIdentifier = table.Column<string>(nullable: false),
                    Progress = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBook", x => new { x.BookId, x.UserIdentifier });
                    table.ForeignKey(
                        name: "FK_UserBook_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "Deleted", "GenreName", "LastUpdated" },
                values: new object[] { "95ea0beb-e05b-4dcf-a4cb-a91cce095d19", false, "Fantasy", new DateTime(2019, 5, 19, 21, 45, 31, 886, DateTimeKind.Local).AddTicks(7716) });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "Deleted", "GenreName", "LastUpdated" },
                values: new object[] { "7a4f9a69-3afd-4dae-829a-ae4691a415db", false, "Sci-Fi", new DateTime(2019, 5, 19, 21, 45, 31, 888, DateTimeKind.Local).AddTicks(3739) });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "Deleted", "Extension", "GenreId", "Isbn", "LastUpdated", "PageNumber", "Title", "UploaderIdentifier" },
                values: new object[,]
                {
                    { "466a182c-dd53-4c27-affb-75fb4ff6e220", "J. R. R. Tolkien", false, "pdf", "95ea0beb-e05b-4dcf-a4cb-a91cce095d19", null, new DateTime(2019, 5, 19, 21, 45, 31, 888, DateTimeKind.Local).AddTicks(8984), 248, "The Fellowship of the Ring", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" },
                    { "3cfb0d9e-1c37-499d-9271-0dd7b17400d3", "J. R. R. Tolkien", false, "pdf", "95ea0beb-e05b-4dcf-a4cb-a91cce095d19", null, new DateTime(2019, 5, 19, 21, 45, 31, 889, DateTimeKind.Local).AddTicks(1641), 249, "The Two Towers", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" },
                    { "b8c55736-422e-49d3-9dc3-d406bdd53d8c", "J. R. R. Tolkien", false, "pdf", "95ea0beb-e05b-4dcf-a4cb-a91cce095d19", null, new DateTime(2019, 5, 19, 21, 45, 31, 889, DateTimeKind.Local).AddTicks(1681), 250, "The Return of the King", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" },
                    { "1a07dc8b-8ef3-4757-a4f2-bdef6b78bd3a", "Alexander Freed", false, "pdf", "7a4f9a69-3afd-4dae-829a-ae4691a415db", null, new DateTime(2019, 5, 19, 21, 45, 31, 889, DateTimeKind.Local).AddTicks(1697), 195, "Rogue One: A Star Wars Story", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" }
                });

            migrationBuilder.InsertData(
                table: "Bookmark",
                columns: new[] { "BookmarkId", "BookId", "Deleted", "LastUpdated", "PageNumber", "UserIdentifier" },
                values: new object[,]
                {
                    { "ad536b7d-08df-48fe-87e4-7253c1c76eac", "466a182c-dd53-4c27-affb-75fb4ff6e220", false, new DateTime(2019, 5, 19, 21, 45, 31, 889, DateTimeKind.Local).AddTicks(5043), 25, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" },
                    { "bbccd264-fb13-421d-8335-c3267bdeaeea", "3cfb0d9e-1c37-499d-9271-0dd7b17400d3", false, new DateTime(2019, 5, 19, 21, 45, 31, 889, DateTimeKind.Local).AddTicks(6589), 37, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" },
                    { "74762732-bc6b-4346-a60a-9117902afbd9", "b8c55736-422e-49d3-9dc3-d406bdd53d8c", false, new DateTime(2019, 5, 19, 21, 45, 31, 889, DateTimeKind.Local).AddTicks(6620), 48, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" }
                });

            migrationBuilder.InsertData(
                table: "UserBook",
                columns: new[] { "BookId", "UserIdentifier", "Deleted", "Progress" },
                values: new object[,]
                {
                    { "466a182c-dd53-4c27-affb-75fb4ff6e220", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba", false, 0 },
                    { "466a182c-dd53-4c27-affb-75fb4ff6e220", "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f", false, 0 },
                    { "3cfb0d9e-1c37-499d-9271-0dd7b17400d3", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba", false, 0 },
                    { "b8c55736-422e-49d3-9dc3-d406bdd53d8c", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba", false, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_BookId",
                table: "Bookmark",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookmark");

            migrationBuilder.DropTable(
                name: "UserBook");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
