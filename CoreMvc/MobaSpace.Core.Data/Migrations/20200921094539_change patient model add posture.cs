using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class changepatientmodeladdposture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "NouvellesDonneesLit",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "NouvellesDonneesHisto",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "NouvellesDonneesBPM",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Historique",
                schema: "mobaspace_data",
                table: "Patients",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true,
                oldDefaultValue: "{\"bed_actions\": null, \"blood_press\": null, \"sleep\": null}");

            migrationBuilder.AddColumn<DateTime>(
                name: "DerniereDateTracker",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NouvellesDonneesTracker",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Posture",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RSSI",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DerniereDateTracker",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NouvellesDonneesTracker",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Posture",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RSSI",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.AlterColumn<bool>(
                name: "NouvellesDonneesLit",
                schema: "mobaspace_data",
                table: "Patients",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "NouvellesDonneesHisto",
                schema: "mobaspace_data",
                table: "Patients",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "NouvellesDonneesBPM",
                schema: "mobaspace_data",
                table: "Patients",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "Historique",
                schema: "mobaspace_data",
                table: "Patients",
                type: "jsonb",
                nullable: true,
                defaultValue: "{\"bed_actions\": null, \"blood_press\": null, \"sleep\": null}",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);
        }
    }
}
