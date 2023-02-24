using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationTableTypeObservableetCheckList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rang",
                schema: "mobaspace_data",
                table: "TypeObservable");

            migrationBuilder.AddColumn<short>(
                name: "Rang",
                schema: "mobaspace_data",
                table: "CheckLists",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rang",
                schema: "mobaspace_data",
                table: "CheckLists");

            migrationBuilder.AddColumn<short>(
                name: "Rang",
                schema: "mobaspace_data",
                table: "TypeObservable",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
