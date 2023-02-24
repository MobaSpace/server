using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class notnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApisCapteurs_OAuth2Apis_ApiId",
                schema: "mobaspace_data",
                table: "ApisCapteurs");

            migrationBuilder.DropForeignKey(
                name: "FK_ApisCapteurs_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "ApisCapteurs");

            migrationBuilder.AlterColumn<long>(
                name: "CapteurId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApisCapteurs_OAuth2Apis_ApiId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                column: "ApiId",
                principalSchema: "mobaspace_data",
                principalTable: "OAuth2Apis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApisCapteurs_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                column: "CapteurId",
                principalSchema: "mobaspace_data",
                principalTable: "Capteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApisCapteurs_OAuth2Apis_ApiId",
                schema: "mobaspace_data",
                table: "ApisCapteurs");

            migrationBuilder.DropForeignKey(
                name: "FK_ApisCapteurs_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "ApisCapteurs");

            migrationBuilder.AlterColumn<long>(
                name: "CapteurId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ApiId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_ApisCapteurs_OAuth2Apis_ApiId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                column: "ApiId",
                principalSchema: "mobaspace_data",
                principalTable: "OAuth2Apis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApisCapteurs_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                column: "CapteurId",
                principalSchema: "mobaspace_data",
                principalTable: "Capteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
