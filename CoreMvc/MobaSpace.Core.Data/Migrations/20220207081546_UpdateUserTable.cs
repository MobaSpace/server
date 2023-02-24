using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class UpdateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UriNetSoins",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserSurname",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "linked2NetSOINS",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UriNetSoins",
                schema: "mobaspace_data",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserSurname",
                schema: "mobaspace_data",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "linked2NetSOINS",
                schema: "mobaspace_data",
                table: "AspNetUsers");
        }
    }
}
