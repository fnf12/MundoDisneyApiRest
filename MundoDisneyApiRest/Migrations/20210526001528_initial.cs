using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoDisneyApiRest.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    idCharacter = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagen = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    edad = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    peso = table.Column<decimal>(type: "decimal(3,1)", nullable: false, defaultValue: 0m),
                    historia = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.idCharacter);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    idGenre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagen = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.idGenre);
                });

            migrationBuilder.CreateTable(
                name: "MovieTV",
                columns: table => new
                {
                    idMovieTV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagen = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    titulo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "date", nullable: false),
                    calificacion = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTV", x => x.idMovieTV);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovieTVs",
                columns: table => new
                {
                    IdGenre = table.Column<int>(type: "int", nullable: false),
                    IdMovieTV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovieTVs", x => new { x.IdGenre, x.IdMovieTV });
                    table.ForeignKey(
                        name: "FK_GenreMovieTVs_Genre_IdGenre",
                        column: x => x.IdGenre,
                        principalTable: "Genre",
                        principalColumn: "idGenre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovieTVs_MovieTV_IdMovieTV",
                        column: x => x.IdMovieTV,
                        principalTable: "MovieTV",
                        principalColumn: "idMovieTV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieTVsCharacters",
                columns: table => new
                {
                    IdMovieTV = table.Column<int>(type: "int", nullable: false),
                    IdCharacter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTVsCharacters", x => new { x.IdCharacter, x.IdMovieTV });
                    table.ForeignKey(
                        name: "FK_MovieTVsCharacters_Character_IdCharacter",
                        column: x => x.IdCharacter,
                        principalTable: "Character",
                        principalColumn: "idCharacter",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieTVsCharacters_MovieTV_IdMovieTV",
                        column: x => x.IdMovieTV,
                        principalTable: "MovieTV",
                        principalColumn: "idMovieTV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovieTVs_IdMovieTV",
                table: "GenreMovieTVs",
                column: "IdMovieTV");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTVsCharacters_IdMovieTV",
                table: "MovieTVsCharacters",
                column: "IdMovieTV");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreMovieTVs");

            migrationBuilder.DropTable(
                name: "MovieTVsCharacters");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "MovieTV");
        }
    }
}
