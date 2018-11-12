using Microsoft.EntityFrameworkCore.Migrations;

namespace SpoiltAPI.Migrations
{
    public partial class ChangeYearToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: "tt0167404",
                column: "Year",
                value: "1999");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Movies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: "tt0167404",
                column: "Year",
                value: 1999);
        }
    }
}
