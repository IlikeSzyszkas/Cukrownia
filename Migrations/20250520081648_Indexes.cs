using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt2.Migrations
{
    /// <inheritdoc />
    public partial class Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Id_dzialu",
                table: "Pracownicy",
                column: "Id_dzialu"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Id_stanowiska",
                table: "Pracownicy",
                column: "Id_stanowiska"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Id_dostawcy",
                table: "Dostawy",
                column: "Id_dostawcy"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Id_operacji_magazyn",
                table: "Magazyn",
                column: "Id_operacji"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Id_operacji_silos",
                table: "Silos",
                column: "Id_operacji"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Id_dostawy_plac_b",
                table: "Plac_buraczany",
                column: "Id_dostawy"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Id_kierownika",
                table: "Pakownia",
                column: "Id_kierownika_zmiany"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Id_kierownika",
                table: "Produktownia",
                column: "Id_kierownika_zmiany"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Id_kupca",
                table: "Sprzedarz",
                column: "Id_kupca"
                );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
