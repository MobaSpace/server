using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationTablePatientetajouttablejour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DerniereDateTracker",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RSSI",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "TensionArtDiastMax_mmHg",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "TensionArtDiastMin_mmHg",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "TensionArtSystMax_mmHg",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "TensionArtSystMin_mmHg",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.AddColumn<double>(
                name: "TempsMaxAllongeJour",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Jours",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(nullable: true),
                    DateJour = table.Column<DateTime>(type: "date", nullable: false),
                    TempsAllongeTotal = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jours_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jours_PatientId",
                schema: "mobaspace_data",
                table: "Jours",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jours",
                schema: "mobaspace_data");

            migrationBuilder.DropColumn(
                name: "TempsMaxAllongeJour",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.AddColumn<DateTime>(
                name: "DerniereDateTracker",
                schema: "mobaspace_data",
                table: "Patients",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RSSI",
                schema: "mobaspace_data",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TensionArtDiastMax_mmHg",
                schema: "mobaspace_data",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TensionArtDiastMin_mmHg",
                schema: "mobaspace_data",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TensionArtSystMax_mmHg",
                schema: "mobaspace_data",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TensionArtSystMin_mmHg",
                schema: "mobaspace_data",
                table: "Patients",
                type: "integer",
                nullable: true);
        }
    }
}
