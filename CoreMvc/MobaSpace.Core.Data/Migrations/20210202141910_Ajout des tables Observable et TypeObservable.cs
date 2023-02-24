using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class AjoutdestablesObservableetTypeObservable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeObservable",
                schema: "mobaspace_data",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeObservable", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Observables",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<long>(nullable: false),
                    PatId = table.Column<long>(nullable: true),
                    Values = table.Column<string>(type: "jsonb", nullable: true),
                    Date = table.Column<DateTime>(type: "TimeStamp", nullable: false),
                    Chambre = table.Column<string>(nullable: true),
                    ObservableTraité = table.Column<bool>(nullable: false),
                    PatientId = table.Column<long>(nullable: true),
                    TypeObservableid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observables_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observables_TypeObservable_TypeObservableid",
                        column: x => x.TypeObservableid,
                        principalSchema: "mobaspace_data",
                        principalTable: "TypeObservable",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Observables_PatientId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Observables_TypeObservableid",
                schema: "mobaspace_data",
                table: "Observables",
                column: "TypeObservableid");

            migrationBuilder.CreateIndex(
                name: "IX_TypeObservable_Type",
                schema: "mobaspace_data",
                table: "TypeObservable",
                column: "Type",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Observables",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "TypeObservable",
                schema: "mobaspace_data");
        }
    }
}
