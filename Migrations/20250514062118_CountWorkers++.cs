using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt2.Migrations
{
    /// <inheritdoc />
    public partial class CountWorkers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LiczbaPracownikow",
                table: "Stanowiska",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LiczbaZmian_pak",
                table: "Pracownicy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LiczbaZmian_prod",
                table: "Pracownicy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LiczbaTransakcji",
                table: "Kupcy",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LiczbaPracownikow",
                table: "Dzialy",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LiczbaPracownikow",
                table: "Stanowiska");

            migrationBuilder.DropColumn(
                name: "LiczbaZmian_pak",
                table: "Pracownicy");

            migrationBuilder.DropColumn(
                name: "LiczbaZmian_prod",
                table: "Pracownicy");

            migrationBuilder.DropColumn(
                name: "LiczbaTransakcji",
                table: "Kupcy");

            migrationBuilder.DropColumn(
                name: "LiczbaPracownikow",
                table: "Dzialy");
        }
    }
}
