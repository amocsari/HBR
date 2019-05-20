using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ubid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook");

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumns: new[] { "BookId", "UserIdentifier" },
                keyValues: new object[] { "3cfb0d9e-1c37-499d-9271-0dd7b17400d3", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumns: new[] { "BookId", "UserIdentifier" },
                keyValues: new object[] { "466a182c-dd53-4c27-affb-75fb4ff6e220", "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f" });

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumns: new[] { "BookId", "UserIdentifier" },
                keyValues: new object[] { "466a182c-dd53-4c27-affb-75fb4ff6e220", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumns: new[] { "BookId", "UserIdentifier" },
                keyValues: new object[] { "b8c55736-422e-49d3-9dc3-d406bdd53d8c", "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.AlterColumn<string>(
                name: "UserIdentifier",
                table: "UserBook",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "UserBookId",
                table: "UserBook",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook",
                column: "UserBookId");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "1a07dc8b-8ef3-4757-a4f2-bdef6b78bd3a",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(7771));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "3cfb0d9e-1c37-499d-9271-0dd7b17400d3",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(7718));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "466a182c-dd53-4c27-affb-75fb4ff6e220",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(5087));

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: "b8c55736-422e-49d3-9dc3-d406bdd53d8c",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "74762732-bc6b-4346-a60a-9117902afbd9",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 6, 10, 40, 357, DateTimeKind.Local).AddTicks(2621));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "ad536b7d-08df-48fe-87e4-7253c1c76eac",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 6, 10, 40, 357, DateTimeKind.Local).AddTicks(1058));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: "bbccd264-fb13-421d-8335-c3267bdeaeea",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 6, 10, 40, 357, DateTimeKind.Local).AddTicks(2591));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: "7a4f9a69-3afd-4dae-829a-ae4691a415db",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 6, 10, 40, 356, DateTimeKind.Local).AddTicks(191));

            migrationBuilder.UpdateData(
                table: "Genre",
                keyColumn: "GenreId",
                keyValue: "95ea0beb-e05b-4dcf-a4cb-a91cce095d19",
                column: "LastUpdated",
                value: new DateTime(2019, 5, 20, 6, 10, 40, 354, DateTimeKind.Local).AddTicks(5304));

            migrationBuilder.InsertData(
                table: "UserBook",
                columns: new[] { "UserBookId", "BookId", "Deleted", "Progress", "UserIdentifier" },
                values: new object[,]
                {
                    { 1, "466a182c-dd53-4c27-affb-75fb4ff6e220", false, 0, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" },
                    { 2, "466a182c-dd53-4c27-affb-75fb4ff6e220", false, 0, "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f" },
                    { 3, "3cfb0d9e-1c37-499d-9271-0dd7b17400d3", false, 0, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" },
                    { 4, "b8c55736-422e-49d3-9dc3-d406bdd53d8c", false, 0, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_BookId",
                table: "UserBook",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_BookId",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "UserBookId",
                table: "UserBook");

            migrationBuilder.AlterColumn<string>(
                name: "UserIdentifier",
                table: "UserBook",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook",
                columns: new[] { "BookId", "UserIdentifier" });

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
        }
    }
}
