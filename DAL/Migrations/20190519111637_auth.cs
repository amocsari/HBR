using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class auth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook");

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
                name: "UserBookId",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserBook");

            migrationBuilder.AddColumn<string>(
                name: "UserIdentifier",
                table: "UserBook",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UploaderIdentifier",
                table: "Book",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook",
                columns: new[] { "BookId", "UserIdentifier" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "LastUpdated", "UploaderIdentifier" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(4559), "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "LastUpdated", "UploaderIdentifier" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(7273), "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 3,
                columns: new[] { "LastUpdated", "UploaderIdentifier" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(7313), "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 4,
                columns: new[] { "LastUpdated", "UploaderIdentifier" },
                values: new object[] { new DateTime(2019, 5, 19, 13, 16, 37, 631, DateTimeKind.Local).AddTicks(7329), "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 16, 37, 632, DateTimeKind.Local).AddTicks(557));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 16, 37, 632, DateTimeKind.Local).AddTicks(2057));

            migrationBuilder.UpdateData(
                table: "Bookmark",
                keyColumn: "BookmarkId",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2019, 5, 19, 13, 16, 37, 632, DateTimeKind.Local).AddTicks(2088));

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

            migrationBuilder.InsertData(
                table: "UserBook",
                columns: new[] { "BookId", "UserIdentifier", "Deleted", "Progress" },
                values: new object[,]
                {
                    { 1, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba", false, 0 },
                    { 1, "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f", false, 0 },
                    { 2, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba", false, 0 },
                    { 3, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba", false, 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_Book_BookId",
                table: "UserBook",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_Book_BookId",
                table: "UserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook");

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumns: new[] { "BookId", "UserIdentifier" },
                keyValues: new object[] { 1, "46c15a9b-9184-46d5-a77e-ca21cd5cbe6f" });

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumns: new[] { "BookId", "UserIdentifier" },
                keyValues: new object[] { 1, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumns: new[] { "BookId", "UserIdentifier" },
                keyValues: new object[] { 2, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.DeleteData(
                table: "UserBook",
                keyColumns: new[] { "BookId", "UserIdentifier" },
                keyValues: new object[] { 3, "87d92da2-13df-47d5-85d7-b3f0fc3d99ba" });

            migrationBuilder.DropColumn(
                name: "UserIdentifier",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "UploaderIdentifier",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "UserBookId",
                table: "UserBook",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook",
                column: "UserBookId");

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
    }
}
