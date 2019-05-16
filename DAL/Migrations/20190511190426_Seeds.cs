using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "UserBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bookmark",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Bookmark",
                columns: new[] { "BookmarkId", "BookId", "Deleted", "PageNumber", "UserId" },
                values: new object[,]
                {
                    { 1, 1, false, 25, 1 },
                    { 2, 2, false, 37, 1 },
                    { 3, 3, false, 48, 1 }
                });

            migrationBuilder.InsertData(
                table: "UserBook",
                columns: new[] { "UserBookId", "BookId", "Deleted", "Progress", "UserId" },
                values: new object[,]
                {
                    { 1, 1, false, 0, 1 },
                    { 2, 2, false, 0, 1 },
                    { 3, 3, false, 0, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumn: "UserBookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumn: "UserBookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumn: "UserBookId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookmark");
        }
    }
}
