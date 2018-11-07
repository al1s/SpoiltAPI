using Microsoft.EntityFrameworkCore.Migrations;

namespace SpoiltAPI.Migrations
{
    public partial class RemovedVotesFromSpoilers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "Spoilers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "Spoilers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Spoilers",
                keyColumn: "ID",
                keyValue: 1,
                column: "Votes",
                value: -45);
        }
    }
}
