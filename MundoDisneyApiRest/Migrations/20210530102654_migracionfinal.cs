using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoDisneyApiRest.Migrations
{
    public partial class migracionfinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovieTVs_Genre_IdGenre",
                table: "GenreMovieTVs");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovieTVs_MovieTV_IdMovieTV",
                table: "GenreMovieTVs");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMovieTVs_IdMovieTV",
                table: "GenreMovieTVs",
                newName: "IX_GenreMovieTVs_MovieTVsIdMovieTV");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                table: "MovieTV",
                type: "date",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    mail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    pass = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.idUser);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovieTVs_Genre_GenresIdGenre",
                table: "GenreMovieTVs",
                column: "IdGenre",
                principalTable: "Genre",
                principalColumn: "idGenre",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovieTVs_MovieTV_MovieTVsIdMovieTV",
                table: "GenreMovieTVs",
                column: "IdMovieTV",
                principalTable: "MovieTV",
                principalColumn: "idMovieTV",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovieTVs_Genre_GenresIdGenre",
                table: "GenreMovieTVs");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovieTVs_MovieTV_MovieTVsIdMovieTV",
                table: "GenreMovieTVs");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMovieTVs_MovieTVsIdMovieTV",
                table: "GenreMovieTVs",
                newName: "IX_GenreMovieTVs_IdMovieTV");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fechaCreacion",
                table: "MovieTV",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovieTVs_Genre_IdGenre",
                table: "GenreMovieTVs",
                column: "IdGenre",
                principalTable: "Genre",
                principalColumn: "idGenre",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovieTVs_MovieTV_IdMovieTV",
                table: "GenreMovieTVs",
                column: "IdMovieTV",
                principalTable: "MovieTV",
                principalColumn: "idMovieTV",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
