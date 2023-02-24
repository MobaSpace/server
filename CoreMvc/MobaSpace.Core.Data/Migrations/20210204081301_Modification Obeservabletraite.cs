using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationObeservabletraite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObservableTraité",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.AddColumn<bool>(
                name: "ObservableTraite",
                schema: "mobaspace_data",
                table: "Observables",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObservableTraite",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.AddColumn<bool>(
                name: "ObservableTraité",
                schema: "mobaspace_data",
                table: "Observables",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
