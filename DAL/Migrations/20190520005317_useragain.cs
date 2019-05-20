using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class useragain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_User_UserId",
                table: "User",
                column: "UserId");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "1a07dc8b-8ef3-4757-a4f2-bdef6b78bd3a",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 53, 16, 965, DateTimeKind.Local).AddTicks(7169));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 53, 16, 965, DateTimeKind.Local).AddTicks(7114));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "466a182c-dd53-4c27-affb-75fb4ff6e220",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 53, 16, 965, DateTimeKind.Local).AddTicks(4456));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 53, 16, 965, DateTimeKind.Local).AddTicks(7153));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "74762732-bc6b-4346-a60a-9117902afbd9",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 53, 16, 966, DateTimeKind.Local).AddTicks(2164));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "ad536b7d-08df-48fe-87e4-7253c1c76eac",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 53, 16, 966, DateTimeKind.Local).AddTicks(431));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "bbccd264-fb13-421d-8335-c3267bdeaeea",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 53, 16, 966, DateTimeKind.Local).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: "7a4f9a69-3afd-4dae-829a-ae4691a415db",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 53, 16, 964, DateTimeKind.Local).AddTicks(9384));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 53, 16, 963, DateTimeKind.Local).AddTicks(3156));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_User_UserId",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "User",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "1a07dc8b-8ef3-4757-a4f2-bdef6b78bd3a",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 30, 55, 607, DateTimeKind.Local).AddTicks(9239));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 30, 55, 607, DateTimeKind.Local).AddTicks(9184));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "466a182c-dd53-4c27-affb-75fb4ff6e220",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 30, 55, 607, DateTimeKind.Local).AddTicks(6498));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 30, 55, 607, DateTimeKind.Local).AddTicks(9223));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "74762732-bc6b-4346-a60a-9117902afbd9",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 30, 55, 608, DateTimeKind.Local).AddTicks(4361));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "ad536b7d-08df-48fe-87e4-7253c1c76eac",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 30, 55, 608, DateTimeKind.Local).AddTicks(2684));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "bbccd264-fb13-421d-8335-c3267bdeaeea",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 30, 55, 608, DateTimeKind.Local).AddTicks(4331));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: "7a4f9a69-3afd-4dae-829a-ae4691a415db",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 30, 55, 607, DateTimeKind.Local).AddTicks(1445));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 30, 55, 605, DateTimeKind.Local).AddTicks(5670));
        }
    }
}
