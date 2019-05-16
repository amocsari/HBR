using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class SampleBook3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[] { 1, "Fantasy" });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[] { 2, "Sci-Fi" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "GenreId", "PageNumber" },
                values: new object[] { 1, 248 });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "GenreId", "PageNumber" },
                values: new object[] { 1, 249 });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 3,
                column: "GenreId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "Deleted", "GenreId", "Isbn", "PageNumber", "Title" },
                values: new object[] { 4, "Alexander Freed", false, 2, null, 195, "Rogue One: A Star Wars Story" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "GenreId", "PageNumber" },
                values: new object[] { null, 250 });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "GenreId", "PageNumber" },
                values: new object[] { null, 250 });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 3,
                column: "GenreId",
                value: null);
        }
    }
}
