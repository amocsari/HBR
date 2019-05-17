using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class lastupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Genre",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Bookmark",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Book",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 17, 4, 14, 1, 461, DateTimeKind.Local).AddTicks(2784));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 17, 4, 14, 1, 461, DateTimeKind.Local).AddTicks(5228));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 17, 4, 14, 1, 461, DateTimeKind.Local).AddTicks(5263));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 17, 4, 14, 1, 461, DateTimeKind.Local).AddTicks(5279));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 17, 4, 14, 1, 461, DateTimeKind.Local).AddTicks(8476));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 17, 4, 14, 1, 462, DateTimeKind.Local).AddTicks(75));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 17, 4, 14, 1, 462, DateTimeKind.Local).AddTicks(104));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 17, 4, 14, 1, 459, DateTimeKind.Local).AddTicks(3324));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 17, 4, 14, 1, 460, DateTimeKind.Local).AddTicks(7691));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Book");
        }
    }
}
