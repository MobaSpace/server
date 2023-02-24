using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class AddRequiredonCapteurinTracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trackers_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "Trackers");

            migrationBuilder.AlterColumn<long>(
                name: "CapteurId",
                schema: "mobaspace_data",
                table: "Trackers",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trackers_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "Trackers",
                column: "CapteurId",
                principalSchema: "mobaspace_data",
                principalTable: "Capteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trackers_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "Trackers");

            migrationBuilder.AlterColumn<long>(
                name: "CapteurId",
                schema: "mobaspace_data",
                table: "Trackers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Trackers_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "Trackers",
                column: "CapteurId",
                principalSchema: "mobaspace_data",
                principalTable: "Capteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
