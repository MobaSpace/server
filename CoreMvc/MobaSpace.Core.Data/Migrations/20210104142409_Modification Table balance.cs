using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationTablebalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdresseMAC",
                schema: "mobaspace_data",
                table: "Balances",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_AdresseMAC",
                schema: "mobaspace_data",
                table: "Balances",
                column: "AdresseMAC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_Nom",
                schema: "mobaspace_data",
                table: "Balances",
                column: "Nom",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Balances_AdresseMAC",
                schema: "mobaspace_data",
                table: "Balances");

            migrationBuilder.DropIndex(
                name: "IX_Balances_Nom",
                schema: "mobaspace_data",
                table: "Balances");

            migrationBuilder.AlterColumn<string>(
                name: "AdresseMAC",
                schema: "mobaspace_data",
                table: "Balances",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
