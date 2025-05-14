using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt2.Migrations
{
    /// <inheritdoc />
    public partial class CountDostawy1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LiczbaDostaw",
                table: "Dostawcy",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LiczbaDostaw",
                table: "Dostawcy");
        }
    }
}
