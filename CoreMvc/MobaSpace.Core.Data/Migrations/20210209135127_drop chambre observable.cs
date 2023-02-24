using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class dropchambreobservable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chambre",
                schema: "mobaspace_data",
                table: "Observables");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Chambre",
                schema: "mobaspace_data",
                table: "Observables",
                type: "text",
                nullable: true);
        }
    }
}
