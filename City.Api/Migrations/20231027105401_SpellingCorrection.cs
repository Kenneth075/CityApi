using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace City.Api.Migrations
{
    public partial class SpellingCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Benin-City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Benin-Ciry");
        }
    }
}
