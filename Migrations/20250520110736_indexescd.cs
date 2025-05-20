using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt2.Migrations
{
    /// <inheritdoc />
    public partial class indexescd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Data_operacji_mag",
                table: "Magazyn",
                column: "Data_operacji"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Data_dostawy",
                table: "Dostawy",
                column: "Data_dostawy"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Data_zmiany_pak",
                table: "Pakownia",
                column: "Data_zmiany"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Data_operacji_plac",
                table: "Plac_buraczany",
                column: "Data_operacji"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Data_pobrania_plac",
                table: "Plac_produktownia",
                column: "Data_pobrania"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Data_zmiany_prod",
                table: "Produktownia",
                column: "Data_zmiany"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Data_skladowania",
                table: "Silos",
                column: "Data_skladowania"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Data_pobrania_silos",
                table: "Silos_pakownia",
                column: "Data_pobrania"
                );
            migrationBuilder.CreateIndex(
                name: "IX_Data_odbioru",
                table: "Sprzedarz",
                column: "Data_odbioru"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
