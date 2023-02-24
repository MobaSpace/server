using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class LabelCorrectionTableNuit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DetailSortie",
                schema: "mobaspace_data",
                table: "Nuits",
                newName: "DetailSorties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DetailSorties",
                schema: "mobaspace_data",
                table: "Nuits",
                newName: "DetailSortie");
        }
    }
}
