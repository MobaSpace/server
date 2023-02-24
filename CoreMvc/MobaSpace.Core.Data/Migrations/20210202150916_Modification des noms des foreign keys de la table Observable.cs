using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationdesnomsdesforeignkeysdelatableObservable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observables_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropForeignKey(
                name: "FK_Observables_TypeObservable_TypeObservableid",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.RenameColumn(
                name: "TypeObservableid",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "PatID");

            migrationBuilder.RenameIndex(
                name: "IX_Observables_TypeObservableid",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "IX_Observables_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Observables_PatientId",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "IX_Observables_PatID");

            migrationBuilder.AlterColumn<long>(
                name: "TypeId",
                schema: "mobaspace_data",
                table: "Observables",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PatID",
                schema: "mobaspace_data",
                table: "Observables",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Observables_TypeObservable_TypeId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "TypeId",
                principalSchema: "mobaspace_data",
                principalTable: "TypeObservable",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observables_Patients_PatID",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropForeignKey(
                name: "FK_Observables_TypeObservable_TypeId",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "TypeObservableid");

            migrationBuilder.RenameColumn(
                name: "PatID",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Observables_TypeId",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "IX_Observables_TypeObservableid");

            migrationBuilder.RenameIndex(
                name: "IX_Observables_PatID",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "IX_Observables_PatientId");

            migrationBuilder.AlterColumn<long>(
                name: "TypeObservableid",
                schema: "mobaspace_data",
                table: "Observables",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "PatientId",
                schema: "mobaspace_data",
                table: "Observables",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Observables_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Observables_TypeObservable_TypeObservableid",
                schema: "mobaspace_data",
                table: "Observables",
                column: "TypeObservableid",
                principalSchema: "mobaspace_data",
                principalTable: "TypeObservable",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
