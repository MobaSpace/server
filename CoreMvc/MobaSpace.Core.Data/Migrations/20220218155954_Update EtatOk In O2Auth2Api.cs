using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class UpdateEtatOkInO2Auth2Api : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EtatOk",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EtatOk",
                schema: "mobaspace_data",
                table: "OAuth2Apis");
        }
    }
}
