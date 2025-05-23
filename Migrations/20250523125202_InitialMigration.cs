using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt2.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dostawcy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Addres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nr_tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ilosc_ha_pola = table.Column<int>(type: "int", nullable: false),
                    LiczbaDostaw = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostawcy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dzialy",
                columns: table => new
                {
                    Id_dzialu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LiczbaPracownikow = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dzialy", x => x.Id_dzialu);
                });

            migrationBuilder.CreateTable(
                name: "Kupcy",
                columns: table => new
                {
                    Id_kupca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nr_tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LiczbaTransakcji = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupcy", x => x.Id_kupca);
                });

            migrationBuilder.CreateTable(
                name: "Stanowiska",
                columns: table => new
                {
                    Id_stanowiska = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LiczbaPracownikow = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanowiska", x => x.Id_stanowiska);
                });

            migrationBuilder.CreateTable(
                name: "Dostawy",
                columns: table => new
                {
                    Id_dostawy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ilosc_towaru = table.Column<int>(type: "int", nullable: false),
                    Data_dostawy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_dostawcy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostawy", x => x.Id_dostawy);
                    table.ForeignKey(
                        name: "FK_Dostawy_Dostawcy_Id_dostawcy",
                        column: x => x.Id_dostawcy,
                        principalTable: "Dostawcy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sprzedarz",
                columns: table => new
                {
                    Id_transakcji = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ilosc_opakowan = table.Column<int>(type: "int", nullable: false),
                    Data_odbioru = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_kupca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprzedarz", x => x.Id_transakcji);
                    table.ForeignKey(
                        name: "FK_Sprzedarz_Kupcy_Id_kupca",
                        column: x => x.Id_kupca,
                        principalTable: "Kupcy",
                        principalColumn: "Id_kupca",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Addres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nr_tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_stanowiska = table.Column<int>(type: "int", nullable: false),
                    Id_dzialu = table.Column<int>(type: "int", nullable: false),
                    LiczbaZmian_prod = table.Column<int>(type: "int", nullable: true),
                    LiczbaZmian_pak = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Dzialy_Id_dzialu",
                        column: x => x.Id_dzialu,
                        principalTable: "Dzialy",
                        principalColumn: "Id_dzialu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Stanowiska_Id_stanowiska",
                        column: x => x.Id_stanowiska,
                        principalTable: "Stanowiska",
                        principalColumn: "Id_stanowiska",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plac_buraczany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_dostawy = table.Column<int>(type: "int", nullable: false),
                    Ilosc_burakow = table.Column<int>(type: "int", nullable: false),
                    Data_operacji = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plac_buraczany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plac_buraczany_Dostawy_Id_dostawy",
                        column: x => x.Id_dostawy,
                        principalTable: "Dostawy",
                        principalColumn: "Id_dostawy",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pakownia",
                columns: table => new
                {
                    Id_partii = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ilosc_towaru_wejscowego = table.Column<int>(type: "int", nullable: false),
                    Ilosc_towaru_wyjscowego = table.Column<int>(type: "int", nullable: false),
                    Id_kierownika_zmiany = table.Column<int>(type: "int", nullable: false),
                    Data_zmiany = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pakownia", x => x.Id_partii);
                    table.ForeignKey(
                        name: "FK_Pakownia_Pracownicy_Id_kierownika_zmiany",
                        column: x => x.Id_kierownika_zmiany,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produktownia",
                columns: table => new
                {
                    Id_partii = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ilosc_towaru_wejscowego = table.Column<int>(type: "int", nullable: false),
                    Ilosc_towaru_wyjscowego = table.Column<int>(type: "int", nullable: false),
                    Id_kierownika_zmiany = table.Column<int>(type: "int", nullable: false),
                    Data_zmiany = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produktownia", x => x.Id_partii);
                    table.ForeignKey(
                        name: "FK_Produktownia_Pracownicy_Id_kierownika_zmiany",
                        column: x => x.Id_kierownika_zmiany,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Magazyn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_operacji = table.Column<int>(type: "int", nullable: false),
                    Ilosc_opakowan = table.Column<int>(type: "int", nullable: false),
                    Data_operacji = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazyn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Magazyn_Pakownia_Id_operacji",
                        column: x => x.Id_operacji,
                        principalTable: "Pakownia",
                        principalColumn: "Id_partii",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plac_produktownia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_dostawy = table.Column<int>(type: "int", nullable: false),
                    Id_partii = table.Column<int>(type: "int", nullable: false),
                    Ilosc_burakow_pobrana = table.Column<int>(type: "int", nullable: false),
                    Data_pobrania = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plac_produktownia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plac_produktownia_Dostawy_Id_dostawy",
                        column: x => x.Id_dostawy,
                        principalTable: "Dostawy",
                        principalColumn: "Id_dostawy",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plac_produktownia_Produktownia_Id_partii",
                        column: x => x.Id_partii,
                        principalTable: "Produktownia",
                        principalColumn: "Id_partii",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Silos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_operacji = table.Column<int>(type: "int", nullable: false),
                    Ilosc_cukru = table.Column<int>(type: "int", nullable: false),
                    Data_skladowania = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Silos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Silos_Produktownia_Id_operacji",
                        column: x => x.Id_operacji,
                        principalTable: "Produktownia",
                        principalColumn: "Id_partii",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Magazyn_sprzedarz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_operacji = table.Column<int>(type: "int", nullable: false),
                    Id_transakcji = table.Column<int>(type: "int", nullable: false),
                    Ilosc_opakowan_sprzedanych = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazyn_sprzedarz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Magazyn_sprzedarz_Magazyn_Id_operacji",
                        column: x => x.Id_operacji,
                        principalTable: "Magazyn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Magazyn_sprzedarz_Sprzedarz_Id_transakcji",
                        column: x => x.Id_transakcji,
                        principalTable: "Sprzedarz",
                        principalColumn: "Id_transakcji",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Silos_pakownia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_operacji = table.Column<int>(type: "int", nullable: false),
                    Id_partii = table.Column<int>(type: "int", nullable: false),
                    Ilosc_cukru_pobrana = table.Column<int>(type: "int", nullable: false),
                    Data_pobrania = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Silos_pakownia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Silos_pakownia_Pakownia_Id_partii",
                        column: x => x.Id_partii,
                        principalTable: "Pakownia",
                        principalColumn: "Id_partii",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Silos_pakownia_Silos_Id_operacji",
                        column: x => x.Id_operacji,
                        principalTable: "Silos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dostawy_Id_dostawcy",
                table: "Dostawy",
                column: "Id_dostawcy");

            migrationBuilder.CreateIndex(
                name: "IX_Magazyn_Id_operacji",
                table: "Magazyn",
                column: "Id_operacji");

            migrationBuilder.CreateIndex(
                name: "IX_Magazyn_sprzedarz_Id_operacji",
                table: "Magazyn_sprzedarz",
                column: "Id_operacji");

            migrationBuilder.CreateIndex(
                name: "IX_Magazyn_sprzedarz_Id_transakcji",
                table: "Magazyn_sprzedarz",
                column: "Id_transakcji");

            migrationBuilder.CreateIndex(
                name: "IX_Pakownia_Id_kierownika_zmiany",
                table: "Pakownia",
                column: "Id_kierownika_zmiany");

            migrationBuilder.CreateIndex(
                name: "IX_Plac_buraczany_Id_dostawy",
                table: "Plac_buraczany",
                column: "Id_dostawy");

            migrationBuilder.CreateIndex(
                name: "IX_Plac_produktownia_Id_dostawy",
                table: "Plac_produktownia",
                column: "Id_dostawy");

            migrationBuilder.CreateIndex(
                name: "IX_Plac_produktownia_Id_partii",
                table: "Plac_produktownia",
                column: "Id_partii");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_Id_dzialu",
                table: "Pracownicy",
                column: "Id_dzialu");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_Id_stanowiska",
                table: "Pracownicy",
                column: "Id_stanowiska");

            migrationBuilder.CreateIndex(
                name: "IX_Produktownia_Id_kierownika_zmiany",
                table: "Produktownia",
                column: "Id_kierownika_zmiany");

            migrationBuilder.CreateIndex(
                name: "IX_Silos_Id_operacji",
                table: "Silos",
                column: "Id_operacji");

            migrationBuilder.CreateIndex(
                name: "IX_Silos_pakownia_Id_operacji",
                table: "Silos_pakownia",
                column: "Id_operacji");

            migrationBuilder.CreateIndex(
                name: "IX_Silos_pakownia_Id_partii",
                table: "Silos_pakownia",
                column: "Id_partii");

            migrationBuilder.CreateIndex(
                name: "IX_Sprzedarz_Id_kupca",
                table: "Sprzedarz",
                column: "Id_kupca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Magazyn_sprzedarz");

            migrationBuilder.DropTable(
                name: "Plac_buraczany");

            migrationBuilder.DropTable(
                name: "Plac_produktownia");

            migrationBuilder.DropTable(
                name: "Silos_pakownia");

            migrationBuilder.DropTable(
                name: "Magazyn");

            migrationBuilder.DropTable(
                name: "Sprzedarz");

            migrationBuilder.DropTable(
                name: "Dostawy");

            migrationBuilder.DropTable(
                name: "Silos");

            migrationBuilder.DropTable(
                name: "Pakownia");

            migrationBuilder.DropTable(
                name: "Kupcy");

            migrationBuilder.DropTable(
                name: "Dostawcy");

            migrationBuilder.DropTable(
                name: "Produktownia");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Dzialy");

            migrationBuilder.DropTable(
                name: "Stanowiska");
        }
    }
}
