using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class CréationCheckList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:periodes", "matin,midi,soir");

            migrationBuilder.CreateTable(
                name: "CheckLists",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(nullable: true),
                    Check_Item = table.Column<string>(nullable: true),
                    programme = table.Column<int>(type: "Periodes[3]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckLists_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckLists_PatientId",
                schema: "mobaspace_data",
                table: "CheckLists",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckLists",
                schema: "mobaspace_data");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:periodes", "matin,midi,soir");
        }
    }
}
