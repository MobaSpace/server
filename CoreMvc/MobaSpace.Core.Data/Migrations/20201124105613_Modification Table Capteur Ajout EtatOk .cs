using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationTableCapteurAjoutEtatOk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumSerie",
                schema: "mobaspace_data",
                table: "Capteurs");

            migrationBuilder.AddColumn<bool>(
                name: "EtatOk",
                schema: "mobaspace_data",
                table: "Capteurs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EtatOk",
                schema: "mobaspace_data",
                table: "Capteurs");

            migrationBuilder.AddColumn<string>(
                name: "NumSerie",
                schema: "mobaspace_data",
                table: "Capteurs",
                type: "text",
                nullable: true);
        }
    }
}
