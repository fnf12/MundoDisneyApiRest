using Microsoft.EntityFrameworkCore.Migrations;

namespace MundoDisneyApiRest.Migrations
{
    public partial class decimalcorrecion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "peso",
                table: "Character",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldDefaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "peso",
                table: "Character",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldDefaultValue: 0m);
        }
    }
}
