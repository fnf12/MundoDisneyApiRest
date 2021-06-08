using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoDisneyApiRest.Migrations
{
    public partial class @base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "peso",
                table: "Character",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)",
                oldDefaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "peso",
                table: "Character",
                type: "decimal(3,1)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldDefaultValue: 0m);
        }
    }
}
