using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Modificationdesnomsdestables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmesC_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "AlarmesC");

            migrationBuilder.DropForeignKey(
                name: "FK_AlarmesC_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "AlarmesC");

            migrationBuilder.DropForeignKey(
                name: "FK_AlarmesC_AspNetUsers_UtilisateurId",
                schema: "mobaspace_data",
                table: "AlarmesC");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactsPatients_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "ContactsPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_JoursCs_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "JoursCs");

            migrationBuilder.DropForeignKey(
                name: "FK_NuitsC_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "NuitsC");

            migrationBuilder.DropForeignKey(
                name: "FK_OAuth2Apis_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "OAuth2Apis");

            migrationBuilder.DropForeignKey(
                name: "FK_ObservablesC_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "ObservablesC");

            migrationBuilder.DropForeignKey(
                name: "FK_ObservablesC_TypeObservable_TypeObservableId",
                schema: "mobaspace_data",
                table: "ObservablesC");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackersC_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "TrackersC");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackersC",
                schema: "mobaspace_data",
                table: "TrackersC");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientsC",
                schema: "mobaspace_data",
                table: "PatientsC");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObservablesC",
                schema: "mobaspace_data",
                table: "ObservablesC");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NuitsC",
                schema: "mobaspace_data",
                table: "NuitsC");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoursCs",
                schema: "mobaspace_data",
                table: "JoursCs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlarmesC",
                schema: "mobaspace_data",
                table: "AlarmesC");

            migrationBuilder.RenameTable(
                name: "TrackersC",
                schema: "mobaspace_data",
                newName: "Trackers",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "PatientsC",
                schema: "mobaspace_data",
                newName: "Patients",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "ObservablesC",
                schema: "mobaspace_data",
                newName: "Observables",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "NuitsC",
                schema: "mobaspace_data",
                newName: "Nuits",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "JoursCs",
                schema: "mobaspace_data",
                newName: "Jours",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "AlarmesC",
                schema: "mobaspace_data",
                newName: "Alarmes",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameIndex(
                name: "IX_TrackersC_CapteurId",
                schema: "mobaspace_data",
                table: "Trackers",
                newName: "IX_Trackers_CapteurId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientsC_NumCh",
                schema: "mobaspace_data",
                table: "Patients",
                newName: "IX_Patients_NumCh");

            migrationBuilder.RenameIndex(
                name: "IX_ObservablesC_TypeObservableId",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "IX_Observables_TypeObservableId");

            migrationBuilder.RenameIndex(
                name: "IX_ObservablesC_PatientId",
                schema: "mobaspace_data",
                table: "Observables",
                newName: "IX_Observables_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_AlarmesC_UtilisateurId",
                schema: "mobaspace_data",
                table: "Alarmes",
                newName: "IX_Alarmes_UtilisateurId");

            migrationBuilder.RenameIndex(
                name: "IX_AlarmesC_PatientId",
                schema: "mobaspace_data",
                table: "Alarmes",
                newName: "IX_Alarmes_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_AlarmesC_CapteurId",
                schema: "mobaspace_data",
                table: "Alarmes",
                newName: "IX_Alarmes_CapteurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trackers",
                schema: "mobaspace_data",
                table: "Trackers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                schema: "mobaspace_data",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Observables",
                schema: "mobaspace_data",
                table: "Observables",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nuits",
                schema: "mobaspace_data",
                table: "Nuits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jours",
                schema: "mobaspace_data",
                table: "Jours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alarmes",
                schema: "mobaspace_data",
                table: "Alarmes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alarmes_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "Alarmes",
                column: "CapteurId",
                principalSchema: "mobaspace_data",
                principalTable: "Capteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alarmes_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Alarmes",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alarmes_AspNetUsers_UtilisateurId",
                schema: "mobaspace_data",
                table: "Alarmes",
                column: "UtilisateurId",
                principalSchema: "mobaspace_data",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_Patients_PatientId",
                schema: "mobaspace_data",
                table: "CheckLists",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactsPatients_Patients_PatientId",
                schema: "mobaspace_data",
                table: "ContactsPatients",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jours_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Jours",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nuits_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Nuits",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OAuth2Apis_Patients_PatientId",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Observables_TypeObservable_TypeObservableId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "TypeObservableId",
                principalSchema: "mobaspace_data",
                principalTable: "TypeObservable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trackers_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "Trackers",
                column: "CapteurId",
                principalSchema: "mobaspace_data",
                principalTable: "Capteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarmes_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "Alarmes");

            migrationBuilder.DropForeignKey(
                name: "FK_Alarmes_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Alarmes");

            migrationBuilder.DropForeignKey(
                name: "FK_Alarmes_AspNetUsers_UtilisateurId",
                schema: "mobaspace_data",
                table: "Alarmes");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_Patients_PatientId",
                schema: "mobaspace_data",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactsPatients_Patients_PatientId",
                schema: "mobaspace_data",
                table: "ContactsPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_Jours_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Jours");

            migrationBuilder.DropForeignKey(
                name: "FK_Nuits_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropForeignKey(
                name: "FK_OAuth2Apis_Patients_PatientId",
                schema: "mobaspace_data",
                table: "OAuth2Apis");

            migrationBuilder.DropForeignKey(
                name: "FK_Observables_Patients_PatientId",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropForeignKey(
                name: "FK_Observables_TypeObservable_TypeObservableId",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropForeignKey(
                name: "FK_Trackers_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "Trackers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trackers",
                schema: "mobaspace_data",
                table: "Trackers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Observables",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nuits",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jours",
                schema: "mobaspace_data",
                table: "Jours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alarmes",
                schema: "mobaspace_data",
                table: "Alarmes");

            migrationBuilder.RenameTable(
                name: "Trackers",
                schema: "mobaspace_data",
                newName: "TrackersC",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "Patients",
                schema: "mobaspace_data",
                newName: "PatientsC",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "Observables",
                schema: "mobaspace_data",
                newName: "ObservablesC",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "Nuits",
                schema: "mobaspace_data",
                newName: "NuitsC",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "Jours",
                schema: "mobaspace_data",
                newName: "JoursCs",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameTable(
                name: "Alarmes",
                schema: "mobaspace_data",
                newName: "AlarmesC",
                newSchema: "mobaspace_data");

            migrationBuilder.RenameIndex(
                name: "IX_Trackers_CapteurId",
                schema: "mobaspace_data",
                table: "TrackersC",
                newName: "IX_TrackersC_CapteurId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_NumCh",
                schema: "mobaspace_data",
                table: "PatientsC",
                newName: "IX_PatientsC_NumCh");

            migrationBuilder.RenameIndex(
                name: "IX_Observables_TypeObservableId",
                schema: "mobaspace_data",
                table: "ObservablesC",
                newName: "IX_ObservablesC_TypeObservableId");

            migrationBuilder.RenameIndex(
                name: "IX_Observables_PatientId",
                schema: "mobaspace_data",
                table: "ObservablesC",
                newName: "IX_ObservablesC_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Alarmes_UtilisateurId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                newName: "IX_AlarmesC_UtilisateurId");

            migrationBuilder.RenameIndex(
                name: "IX_Alarmes_PatientId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                newName: "IX_AlarmesC_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Alarmes_CapteurId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                newName: "IX_AlarmesC_CapteurId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackersC",
                schema: "mobaspace_data",
                table: "TrackersC",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientsC",
                schema: "mobaspace_data",
                table: "PatientsC",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObservablesC",
                schema: "mobaspace_data",
                table: "ObservablesC",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NuitsC",
                schema: "mobaspace_data",
                table: "NuitsC",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoursCs",
                schema: "mobaspace_data",
                table: "JoursCs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlarmesC",
                schema: "mobaspace_data",
                table: "AlarmesC",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmesC_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                column: "CapteurId",
                principalSchema: "mobaspace_data",
                principalTable: "Capteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmesC_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmesC_AspNetUsers_UtilisateurId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                column: "UtilisateurId",
                principalSchema: "mobaspace_data",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckLists_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "CheckLists",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactsPatients_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "ContactsPatients",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoursCs_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "JoursCs",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NuitsC_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "NuitsC",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OAuth2Apis_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObservablesC_PatientsC_PatientId",
                schema: "mobaspace_data",
                table: "ObservablesC",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObservablesC_TypeObservable_TypeObservableId",
                schema: "mobaspace_data",
                table: "ObservablesC",
                column: "TypeObservableId",
                principalSchema: "mobaspace_data",
                principalTable: "TypeObservable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackersC_Capteurs_CapteurId",
                schema: "mobaspace_data",
                table: "TrackersC",
                column: "CapteurId",
                principalSchema: "mobaspace_data",
                principalTable: "Capteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
