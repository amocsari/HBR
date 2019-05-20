using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class usersstill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "1a07dc8b-8ef3-4757-a4f2-bdef6b78bd3a",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 54, 37, 857, DateTimeKind.Local).AddTicks(9365));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 54, 37, 857, DateTimeKind.Local).AddTicks(9311));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "466a182c-dd53-4c27-affb-75fb4ff6e220",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 54, 37, 857, DateTimeKind.Local).AddTicks(6622));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 54, 37, 857, DateTimeKind.Local).AddTicks(9349));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "74762732-bc6b-4346-a60a-9117902afbd9",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 54, 37, 858, DateTimeKind.Local).AddTicks(4274));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "ad536b7d-08df-48fe-87e4-7253c1c76eac",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 54, 37, 858, DateTimeKind.Local).AddTicks(2668));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "bbccd264-fb13-421d-8335-c3267bdeaeea",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 54, 37, 858, DateTimeKind.Local).AddTicks(4244));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: "7a4f9a69-3afd-4dae-829a-ae4691a415db",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 54, 37, 857, DateTimeKind.Local).AddTicks(1060));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 2, 54, 37, 855, DateTimeKind.Local).AddTicks(5618));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserName", "Password", "UserId" },
                values: new object[,]
                {
                    { "amocsari", "12Wasd34", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" },
                    { "teszt", "12Wasd34", "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumns: new[] { "UserName", "Password" },
                keyValues: new object[] { "amocsari", "12Wasd34" });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumns: new[] { "UserName", "Password" },
                keyValues: new object[] { "teszt", "12Wasd34" });

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
    }
}
