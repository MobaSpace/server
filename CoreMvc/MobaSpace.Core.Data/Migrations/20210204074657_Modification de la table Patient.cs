using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationdelatablePatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Values",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "Valeurs");

            migrationBuilder.AddColumn<long>(
                name: "NumCh",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumCh",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Valeurs",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "Values");
        }
    }
}
