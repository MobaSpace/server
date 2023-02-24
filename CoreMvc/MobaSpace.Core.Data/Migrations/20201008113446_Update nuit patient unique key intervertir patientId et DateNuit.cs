using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class UpdatenuitpatientuniquekeyintervertirpatientIdetDateNuit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Nuits_PatientId",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropIndex(
                name: "U_Date_Patient",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.CreateIndex(
                name: "U_Date_Patient",
                schema: "mobaspace_data",
                table: "Nuits",
                columns: new[] { "PatientId", "DateNuit" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "U_Date_Patient",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.CreateIndex(
                name: "IX_Nuits_PatientId",
                schema: "mobaspace_data",
                table: "Nuits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "U_Date_Patient",
                schema: "mobaspace_data",
                table: "Nuits",
                columns: new[] { "DateNuit", "PatientId" },
                unique: true);
        }
    }
}
