using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Updatenuitpatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IAH",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateNuit",
                schema: "mobaspace_data",
                table: "Nuits",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateNuit",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.AddColumn<int>(
                name: "IAH",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "integer",
                nullable: true);
        }
    }
}
