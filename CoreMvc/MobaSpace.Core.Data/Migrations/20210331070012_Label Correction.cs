using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class LabelCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code_retour",
                schema: "mobaspace_data",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "détail_retour",
                schema: "mobaspace_data",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "NuitTraite",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.RenameColumn(
                name: "date",
                schema: "mobaspace_data",
                table: "Transactions",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "CodeRetour",
                schema: "mobaspace_data",
                table: "Transactions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DetailRetour",
                schema: "mobaspace_data",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NuitTraitee",
                schema: "mobaspace_data",
                table: "Nuits",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeRetour",
                schema: "mobaspace_data",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DetailRetour",
                schema: "mobaspace_data",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "NuitTraitee",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "mobaspace_data",
                table: "Transactions",
                newName: "date");

            migrationBuilder.AddColumn<string>(
                name: "code_retour",
                schema: "mobaspace_data",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "détail_retour",
                schema: "mobaspace_data",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NuitTraite",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
