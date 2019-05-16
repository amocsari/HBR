using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SampleBook2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "Deleted", "GenreId", "Isbn", "PageNumber", "Title" },
                values: new object[] { 2, "J. R. R. Tolkien", false, null, null, 250, "The Two Towers" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "Deleted", "GenreId", "Isbn", "PageNumber", "Title" },
                values: new object[] { 3, "J. R. R. Tolkien", false, null, null, 250, "The Return of the King" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 3);
        }
    }
}
