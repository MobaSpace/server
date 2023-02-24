using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class UpdateTransactionNuitJour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "code_retour",
                schema: "mobaspace_data",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URI",
                schema: "mobaspace_data",
                table: "Transactions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "NuitTraite",
                schema: "mobaspace_data",
                table: "Nuits",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "JourTraite",
                schema: "mobaspace_data",
                table: "Jours",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_URI",
                schema: "mobaspace_data",
                table: "Transactions",
                column: "URI",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_URI",
                schema: "mobaspace_data",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "URI",
                schema: "mobaspace_data",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "NuitTraite",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "JourTraite",
                schema: "mobaspace_data",
                table: "Jours");

            migrationBuilder.AlterColumn<string>(
                name: "code_retour",
                schema: "mobaspace_data",
                table: "Transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
