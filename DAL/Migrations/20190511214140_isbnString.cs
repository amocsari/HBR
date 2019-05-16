using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class isbnString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Book",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Isbn",
                table: "Book",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
