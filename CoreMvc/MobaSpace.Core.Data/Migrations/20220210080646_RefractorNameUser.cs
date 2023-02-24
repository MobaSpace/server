using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class RefractorNameUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "linked2NetSOINS",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                newName: "Linked2NetSOINS");

            migrationBuilder.RenameColumn(
                name: "UriNetSoins",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                newName: "UriNetSOINS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UriNetSOINS",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                newName: "UriNetSoins");

            migrationBuilder.RenameColumn(
                name: "Linked2NetSOINS",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                newName: "linked2NetSOINS");
        }
    }
}
