using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Changecolumntype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DerniereDateTraitee",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.AddColumn<long>(
                name: "DernierIndiceTraite",
                schema: "mobaspace_data",
                table: "Patients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DernierIndiceTraite",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.AddColumn<DateTime>(
                name: "DerniereDateTraitee",
                schema: "mobaspace_data",
                table: "Patients",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
