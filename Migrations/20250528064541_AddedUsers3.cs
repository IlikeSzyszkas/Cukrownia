using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt2.Migrations
{
    /// <inheritdoc />
    public partial class AddedUsers3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PracownikId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PracownikId",
                table: "Users",
                column: "PracownikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Pracownicy_PracownikId",
                table: "Users",
                column: "PracownikId",
                principalTable: "Pracownicy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Pracownicy_PracownikId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PracownikId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PracownikId",
                table: "Users");
        }
    }
}
