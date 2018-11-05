using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpoiltAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Genre = table.Column<string>(nullable: true),
                    Plot = table.Column<string>(nullable: true),
                    Poster = table.Column<string>(nullable: true),
                    IMDBID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Spoilers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    SpoilerText = table.Column<string>(nullable: true),
                    Votes = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    MovieID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spoilers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Spoilers_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "ID", "Genre", "IMDBID", "Plot", "Poster", "Title", "Year" },
                values: new object[] { 1, "Drama, Thriller, Mystery", "tt0167404", "A boy who communicates with spirits seeks the help of a disheartened child psychologist.", "https://m.media-amazon.com/images/M/MV5BMWM4NTFhYjctNzUyNi00NGMwLTk3NTYtMDIyNTZmMzRlYmQyXkEyXkFqcGdeQXVyMTAwMzUyOTc@._V1_SX300.jpg", "The Sixth Sense", 1999 });

            migrationBuilder.InsertData(
                table: "Spoilers",
                columns: new[] { "ID", "Created", "MovieID", "SpoilerText", "UserName", "Votes" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bruce Willis was DEAD THE WHOLE TIME!!!!!", "Stairmaster", -45 });

            migrationBuilder.CreateIndex(
                name: "IX_Spoilers_MovieID",
                table: "Spoilers",
                column: "MovieID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spoilers");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
