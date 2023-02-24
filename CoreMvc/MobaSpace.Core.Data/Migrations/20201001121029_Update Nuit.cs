using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class UpdateNuit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DureeReveil",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "NbSortie",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFin",
                schema: "mobaspace_data",
                table: "Nuits",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DureeReveilAuLit",
                schema: "mobaspace_data",
                table: "Nuits",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DureeReveilHorsLit",
                schema: "mobaspace_data",
                table: "Nuits",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DureeSommeil",
                schema: "mobaspace_data",
                table: "Nuits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NbSorties",
                schema: "mobaspace_data",
                table: "Nuits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DureeReveilAuLit",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "DureeReveilHorsLit",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "DureeSommeil",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "NbSorties",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFin",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DureeReveil",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NbSortie",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "integer",
                nullable: true);
        }
    }
}
