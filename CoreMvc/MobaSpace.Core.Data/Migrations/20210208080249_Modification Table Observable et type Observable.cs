using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationTableObservableettypeObservable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observables_Patients_PatID",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "mobaspace_data",
                table: "TypeObservable",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PatID",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "PatId");

            migrationBuilder.RenameIndex(
                name: "IX_Observables_PatID",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "IX_Observables_PatId");

            migrationBuilder.AddColumn<short>(
                name: "Rang",
                schema: "mobaspace_data",
                table: "TypeObservable",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<long>(
                name: "PatId",
                schema: "mobaspace_data",
                table: "Observables",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Observables_Patients_PatId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "PatId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observables_Patients_PatId",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropColumn(
                name: "Rang",
                schema: "mobaspace_data",
                table: "TypeObservable");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "mobaspace_data",
                table: "TypeObservable",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PatId",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "PatID");

            migrationBuilder.RenameIndex(
                name: "IX_Observables_PatId",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "IX_Observables_PatID");

            migrationBuilder.AlterColumn<long>(
                name: "PatID",
                schema: "mobaspace_data",
                table: "Observables",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Observables_Patients_PatID",
                schema: "mobaspace_data",
                table: "Observables",
                column: "PatID",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
