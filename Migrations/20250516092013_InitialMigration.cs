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
                    Id_dostawcy = table.Column<int>(type: "int", nullable: false),
                    DostawcaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dostawy", x => x.Id_dostawy);
                    table.ForeignKey(
                        name: "FK_Dostawy_Dostawcy_DostawcaId",
                        column: x => x.DostawcaId,
                        principalTable: "Dostawcy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sprzedarz",
                columns: table => new
                {
                    Id_transakcji = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ilosc_opakowan = table.Column<int>(type: "int", nullable: false),
                    Data_odbioru = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_kupca = table.Column<int>(type: "int", nullable: false),
                    KupiecId_kupca = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprzedarz", x => x.Id_transakcji);
                    table.ForeignKey(
                        name: "FK_Sprzedarz_Kupcy_KupiecId_kupca",
                        column: x => x.KupiecId_kupca,
                        principalTable: "Kupcy",
                        principalColumn: "Id_kupca");
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
                    StanowiskoId_stanowiska = table.Column<int>(type: "int", nullable: true),
                    Id_dzialu = table.Column<int>(type: "int", nullable: false),
                    DzialId_dzialu = table.Column<int>(type: "int", nullable: true),
                    LiczbaZmian_prod = table.Column<int>(type: "int", nullable: true),
                    LiczbaZmian_pak = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pracownicy_Dzialy_DzialId_dzialu",
                        column: x => x.DzialId_dzialu,
                        principalTable: "Dzialy",
                        principalColumn: "Id_dzialu");
                    table.ForeignKey(
                        name: "FK_Pracownicy_Stanowiska_StanowiskoId_stanowiska",
                        column: x => x.StanowiskoId_stanowiska,
                        principalTable: "Stanowiska",
                        principalColumn: "Id_stanowiska");
                });

            migrationBuilder.CreateTable(
                name: "Magazyn_sprzedarz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_operacji = table.Column<int>(type: "int", nullable: false),
                    OperacjaId = table.Column<int>(type: "int", nullable: false),
                    Id_transakcji = table.Column<int>(type: "int", nullable: false),
                    TransakcjaId_transakcji = table.Column<int>(type: "int", nullable: true),
                    Ilosc_opakowan_sprzedanych = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazyn_sprzedarz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Magazyn_sprzedarz_Magazyn_OperacjaId",
                        column: x => x.OperacjaId,
                        principalTable: "Magazyn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Magazyn_sprzedarz_Sprzedarz_TransakcjaId_transakcji",
                        column: x => x.TransakcjaId_transakcji,
                        principalTable: "Sprzedarz",
                        principalColumn: "Id_transakcji");
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
                    Kierownik_zmianyId = table.Column<int>(type: "int", nullable: true),
                    Data_zmiany = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pakownia", x => x.Id_partii);
                    table.ForeignKey(
                        name: "FK_Pakownia_Pracownicy_Kierownik_zmianyId",
                        column: x => x.Kierownik_zmianyId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
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
                    Kierownik_zmianyId = table.Column<int>(type: "int", nullable: true),
                    Data_zmiany = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produktownia", x => x.Id_partii);
                    table.ForeignKey(
                        name: "FK_Produktownia_Pracownicy_Kierownik_zmianyId",
                        column: x => x.Kierownik_zmianyId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dostawy_DostawcaId",
                table: "Dostawy",
                column: "DostawcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Magazyn_sprzedarz_OperacjaId",
                table: "Magazyn_sprzedarz",
                column: "OperacjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Magazyn_sprzedarz_TransakcjaId_transakcji",
                table: "Magazyn_sprzedarz",
                column: "TransakcjaId_transakcji");

            migrationBuilder.CreateIndex(
                name: "IX_Pakownia_Kierownik_zmianyId",
                table: "Pakownia",
                column: "Kierownik_zmianyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_DzialId_dzialu",
                table: "Pracownicy",
                column: "DzialId_dzialu");

            migrationBuilder.CreateIndex(
                name: "IX_Pracownicy_StanowiskoId_stanowiska",
                table: "Pracownicy",
                column: "StanowiskoId_stanowiska");

            migrationBuilder.CreateIndex(
                name: "IX_Produktownia_Kierownik_zmianyId",
                table: "Produktownia",
                column: "Kierownik_zmianyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprzedarz_KupiecId_kupca",
                table: "Sprzedarz",
                column: "KupiecId_kupca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dostawy");

            migrationBuilder.DropTable(
                name: "Magazyn_sprzedarz");

            migrationBuilder.DropTable(
                name: "Pakownia");

            migrationBuilder.DropTable(
                name: "Plac_buraczany");

            migrationBuilder.DropTable(
                name: "Plac_produktownia");

            migrationBuilder.DropTable(
                name: "Produktownia");

            migrationBuilder.DropTable(
                name: "Silos");

            migrationBuilder.DropTable(
                name: "Silos_pakownia");

            migrationBuilder.DropTable(
                name: "Dostawcy");

            migrationBuilder.DropTable(
                name: "Magazyn");

            migrationBuilder.DropTable(
                name: "Sprzedarz");

            migrationBuilder.DropTable(
                name: "Pracownicy");

            migrationBuilder.DropTable(
                name: "Kupcy");

            migrationBuilder.DropTable(
                name: "Dzialy");

            migrationBuilder.DropTable(
                name: "Stanowiska");
        }
    }
}
