using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationTablePatientettableJour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jours_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Jours");

            migrationBuilder.DropIndex(
                name: "IX_Jours_PatientId",
                schema: "mobaspace_data",
                table: "Jours");

            migrationBuilder.DropColumn(
                name: "NouvellesDonneesBPM",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NouvellesDonneesTracker",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.AlterColumn<int>(
                name: "TempsMaxAllongeJour",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CumulTempsAllonge",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TempsAllongeTotal",
                schema: "mobaspace_data",
                table: "Jours",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PatientId",
                schema: "mobaspace_data",
                table: "Jours",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "U_Date_J_Patient",
                schema: "mobaspace_data",
                table: "Jours",
                columns: new[] { "PatientId", "DateJour" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jours_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Jours",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jours_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Jours");

            migrationBuilder.DropIndex(
                name: "U_Date_J_Patient",
                schema: "mobaspace_data",
                table: "Jours");

            migrationBuilder.DropColumn(
                name: "CumulTempsAllonge",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.AlterColumn<double>(
                name: "TempsMaxAllongeJour",
                schema: "mobaspace_data",
                table: "Patients",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NouvellesDonneesBPM",
                schema: "mobaspace_data",
                table: "Patients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NouvellesDonneesTracker",
                schema: "mobaspace_data",
                table: "Patients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "TempsAllongeTotal",
                schema: "mobaspace_data",
                table: "Jours",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PatientId",
                schema: "mobaspace_data",
                table: "Jours",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Jours_PatientId",
                schema: "mobaspace_data",
                table: "Jours",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jours_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Jours",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
