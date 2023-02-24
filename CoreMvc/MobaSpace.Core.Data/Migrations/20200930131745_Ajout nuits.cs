using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Ajoutnuits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nuits",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateDebut = table.Column<DateTime>(nullable: false),
                    DateFin = table.Column<DateTime>(nullable: true),
                    ScoreNuit = table.Column<int>(nullable: true),
                    NbReveils = table.Column<int>(nullable: true),
                    NbSortie = table.Column<int>(nullable: true),
                    DureeReveil = table.Column<TimeSpan>(nullable: true),
                    IAH = table.Column<int>(nullable: true),
                    FCMin = table.Column<int>(nullable: true),
                    FCMoy = table.Column<int>(nullable: true),
                    FCMax = table.Column<int>(nullable: true),
                    FRMin = table.Column<int>(nullable: true),
                    FRMoy = table.Column<int>(nullable: true),
                    FRMax = table.Column<int>(nullable: true),
                    PatientId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nuits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nuits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nuits_PatientId",
                schema: "mobaspace_data",
                table: "Nuits",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nuits",
                schema: "mobaspace_data");
        }
    }
}
