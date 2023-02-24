using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Updatenuitpatientuniquekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateNuit",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateIndex(
                name: "U_Date_Patient",
                schema: "mobaspace_data",
                table: "Nuits",
                columns: new[] { "DateNuit", "PatientId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "U_Date_Patient",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateNuit",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
