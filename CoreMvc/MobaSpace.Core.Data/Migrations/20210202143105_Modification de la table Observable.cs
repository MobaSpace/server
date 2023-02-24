using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationdelatableObservable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatId",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropColumn(
                name: "TypeId",
                schema: "mobaspace_data",
                table: "Observables");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PatId",
                schema: "mobaspace_data",
                table: "Observables",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                schema: "mobaspace_data",
                table: "Observables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
