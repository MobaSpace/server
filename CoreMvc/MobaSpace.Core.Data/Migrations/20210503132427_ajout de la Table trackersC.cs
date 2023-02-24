using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ajoutdelaTabletrackersC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackersC",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key_id = table.Column<long>(nullable: false),
                    nonce = table.Column<byte[]>(nullable: true),
                    LecturesWifi = table.Column<string>(type: "jsonb", nullable: true),
                    NbPas = table.Column<byte[]>(nullable: true),
                    AccVector = table.Column<double[]>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    CapteurId = table.Column<long>(nullable: false),
                    Power = table.Column<double>(nullable: false),
                    Traite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackersC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackersC_Capteurs_CapteurId",
                        column: x => x.CapteurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Capteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackersC_CapteurId",
                schema: "mobaspace_data",
                table: "TrackersC",
                column: "CapteurId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackersC",
                schema: "mobaspace_data");
        }
    }
}
