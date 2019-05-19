using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class bookmarkAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookmark");

            migrationBuilder.AddColumn<string>(
                name: "UserIdentifier",
                table: "Bookmark",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 29, 35, 447, DateTimeKind.Local).AddTicks(5423));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 29, 35, 447, DateTimeKind.Local).AddTicks(8118));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 29, 35, 447, DateTimeKind.Local).AddTicks(8157));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 29, 35, 447, DateTimeKind.Local).AddTicks(8174));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 1,
                columns: new[] { "LastUpdated", "UserIdentifier" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 29, 35, 448, DateTimeKind.Local).AddTicks(1329), "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 2,
                columns: new[] { "LastUpdated", "UserIdentifier" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 29, 35, 448, DateTimeKind.Local).AddTicks(2844), "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 3,
                columns: new[] { "LastUpdated", "UserIdentifier" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 29, 35, 448, DateTimeKind.Local).AddTicks(2876), "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 29, 35, 445, DateTimeKind.Local).AddTicks(5583));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 29, 35, 447, DateTimeKind.Local).AddTicks(98));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdentifier",
                table: "Bookmark");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bookmark",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(4559));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(7273));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(7313));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(7329));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 1,
                columns: new[] { "LastUpdated", "UserId" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 16, 37, 632, DateTimeKind.Local).AddTicks(557), 1 });

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 2,
                columns: new[] { "LastUpdated", "UserId" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 16, 37, 632, DateTimeKind.Local).AddTicks(2057), 1 });

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 3,
                columns: new[] { "LastUpdated", "UserId" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 16, 37, 632, DateTimeKind.Local).AddTicks(2088), 1 });

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 16, 37, 629, DateTimeKind.Local).AddTicks(5069));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 16, 37, 630, DateTimeKind.Local).AddTicks(9288));
        }
    }
}
