using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class AddTrackersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trackers",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LecturesWifi = table.Column<string>(nullable: true),
                    NbPas = table.Column<long>(nullable: false),
                    AccVector = table.Column<double[]>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    CapteurId = table.Column<long>(nullable: true),
                    Traite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trackers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trackers_Capteurs_CapteurId",
                        column: x => x.CapteurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Capteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Capteurs_Identifiant",
                schema: "mobaspace_data",
                table: "Capteurs",
                column: "Identifiant",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trackers_CapteurId",
                schema: "mobaspace_data",
                table: "Trackers",
                column: "CapteurId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trackers",
                schema: "mobaspace_data");

            migrationBuilder.DropIndex(
                name: "IX_Capteurs_Identifiant",
                schema: "mobaspace_data",
                table: "Capteurs");
        }
    }
}
