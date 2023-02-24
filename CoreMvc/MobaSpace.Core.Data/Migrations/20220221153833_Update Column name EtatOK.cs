using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class UpdateColumnnameEtatOK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EtatOk",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                newName: "EtatOK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EtatOK",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                newName: "EtatOk");
        }
    }
}
