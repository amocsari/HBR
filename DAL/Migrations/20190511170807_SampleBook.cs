using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SampleBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Book",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Book",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PageNumber",
                table: "Book",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "Deleted", "GenreId", "Isbn", "PageNumber", "Title" },
                values: new object[] { 1, "J. R. R. Tolkien", false, null, null, 250, "The Fellowship of the Ring" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_BookId",
                table: "Bookmark",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genre_GenreId",
                table: "Book",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Book_BookId",
                table: "Bookmark",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genre_GenreId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Book_BookId",
                table: "Bookmark");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_BookId",
                table: "Bookmark");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId",
                table: "Book");

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "PageNumber",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Book",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
