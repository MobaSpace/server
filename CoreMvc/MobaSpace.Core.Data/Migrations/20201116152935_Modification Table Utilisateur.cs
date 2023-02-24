using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationTableUtilisateur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Appel",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SMS",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Appel",
                schema: "mobaspace_data",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Mail",
                schema: "mobaspace_data",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SMS",
                schema: "mobaspace_data",
                table: "AspNetUsers");
        }
    }
}
