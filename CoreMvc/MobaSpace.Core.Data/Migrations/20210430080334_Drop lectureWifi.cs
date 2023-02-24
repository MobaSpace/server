using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class DroplectureWifi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LecturesWifi",
                schema: "mobaspace_data",
                table: "Trackers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LecturesWifi",
                schema: "mobaspace_data",
                table: "Trackers",
                type: "text",
                nullable: true);
        }
    }
}
