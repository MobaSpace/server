using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class UpdateUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAlarmes",
                schema: "mobaspace_data",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SMS",
                schema: "mobaspace_data",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CanalNotif",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanalNotif",
                schema: "mobaspace_data",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "EmailAlarmes",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SMS",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
