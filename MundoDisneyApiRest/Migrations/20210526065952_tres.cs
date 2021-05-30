using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoDisneyApiRest.Migrations
{
    public partial class tres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTVsCharacters_Character_IdCharacter",
                table: "MovieTVsCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieTVsCharacters_MovieTV_IdMovieTV",
                table: "MovieTVsCharacters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieTVsCharacters",
                table: "MovieTVsCharacters");

            migrationBuilder.DropIndex(
                name: "IX_MovieTVsCharacters_IdMovieTV",
                table: "MovieTVsCharacters");

            migrationBuilder.RenameTable(
                name: "MovieTVsCharacters",
                newName: "MovieTVsCharacter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieTVsCharacter",
                table: "MovieTVsCharacter",
                columns: new[] { "IdMovieTV", "IdCharacter" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieTVsCharacter_CharactersIdCharacter",
                table: "MovieTVsCharacter",
                column: "IdCharacter");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTVsCharacter_Character_CharactersIdCharacter",
                table: "MovieTVsCharacter",
                column: "IdCharacter",
                principalTable: "Character",
                principalColumn: "idCharacter",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTVsCharacter_MovieTV_MovieTVsIdMovieTV",
                table: "MovieTVsCharacter",
                column: "IdMovieTV",
                principalTable: "MovieTV",
                principalColumn: "idMovieTV",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTVsCharacter_Character_CharactersIdCharacter",
                table: "MovieTVsCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieTVsCharacter_MovieTV_MovieTVsIdMovieTV",
                table: "MovieTVsCharacter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieTVsCharacter",
                table: "MovieTVsCharacter");

            migrationBuilder.DropIndex(
                name: "IX_MovieTVsCharacter_CharactersIdCharacter",
                table: "MovieTVsCharacter");

            migrationBuilder.RenameTable(
                name: "MovieTVsCharacter",
                newName: "MovieTVsCharacters");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieTVsCharacters",
                table: "MovieTVsCharacters",
                columns: new[] { "IdCharacter", "IdMovieTV" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieTVsCharacters_IdMovieTV",
                table: "MovieTVsCharacters",
                column: "IdMovieTV");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTVsCharacters_Character_IdCharacter",
                table: "MovieTVsCharacters",
                column: "IdCharacter",
                principalTable: "Character",
                principalColumn: "idCharacter",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTVsCharacters_MovieTV_IdMovieTV",
                table: "MovieTVsCharacters",
                column: "IdMovieTV",
                principalTable: "MovieTV",
                principalColumn: "idMovieTV",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
