using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Modificationdesclefsetrangeresetsuppressiondesanciennestables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmesC_Patients_PatientId",
                schema: "mobaspace_data",
                table: "AlarmesC");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckLists_Patients_PatientId",
                schema: "mobaspace_data",
                table: "CheckLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactsPatients_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactsPatients_Patients_PatientId",
                schema: "mobaspace_data",
                table: "ContactsPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_JoursC_Patients_PatientId",
                schema: "mobaspace_data",
                table: "JoursC");

            migrationBuilder.DropForeignKey(
                name: "FK_NuitsC_Patients_PatientId",
                schema: "mobaspace_data",
                table: "NuitsC");

            migrationBuilder.DropForeignKey(
                name: "FK_OAuth2Apis_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis");

            migrationBuilder.DropForeignKey(
                name: "FK_OAuth2Apis_Patients_PatientId",
                schema: "mobaspace_data",
                table: "OAuth2Apis");

            migrationBuilder.DropForeignKey(
                name: "FK_ObservablesC_Patients_PatientId",
                schema: "mobaspace_data",
                table: "ObservablesC");

            migrationBuilder.DropTable(
                name: "Alarmes",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "Jours",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "Nuits",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "Observables",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "Trackers",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "mobaspace_data");

            migrationBuilder.DropIndex(
                name: "IX_OAuth2Apis_PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis");

            migrationBuilder.DropIndex(
                name: "IX_ContactsPatients_PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoursC",
                schema: "mobaspace_data",
                table: "JoursC");

            migrationBuilder.DropColumn(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis");

            migrationBuilder.DropColumn(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients");

            migrationBuilder.RenameTable(
                name: "JoursC",
                schema: "mobaspace_data",
                newName: "JoursCs",
                newSchema: "mobaspace_data");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoursCs",
                schema: "mobaspace_data",
                table: "JoursCs",
                column: "Id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmesC_PatientsC_PatientId",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoursCs",
                schema: "mobaspace_data",
                table: "JoursCs");

            migrationBuilder.RenameTable(
                name: "JoursCs",
                schema: "mobaspace_data",
                newName: "JoursC",
                newSchema: "mobaspace_data");

            migrationBuilder.AddColumn<long>(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoursC",
                schema: "mobaspace_data",
                table: "JoursC",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Chambre = table.Column<string>(type: "text", nullable: false),
                    CheminPhoto = table.Column<string>(type: "text", nullable: true),
                    Coucher_h = table.Column<int>(type: "integer", nullable: true),
                    Coucher_min = table.Column<int>(type: "integer", nullable: true),
                    CumulTempsAllonge = table.Column<int>(type: "integer", nullable: true),
                    DernierCoucher = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DernierIndiceTraite = table.Column<long>(type: "bigint", nullable: true),
                    DernierLever = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DureeMaxHorsLit_min = table.Column<int>(type: "integer", nullable: true),
                    FreqCardiaqueMax_bpm = table.Column<int>(type: "integer", nullable: true),
                    FreqCardiaqueMin_bpm = table.Column<int>(type: "integer", nullable: true),
                    FreqRespMax_bpm = table.Column<int>(type: "integer", nullable: true),
                    FreqRespMin_bpm = table.Column<int>(type: "integer", nullable: true),
                    Historique = table.Column<string>(type: "jsonb", nullable: true),
                    Lever_h = table.Column<int>(type: "integer", nullable: true),
                    Lever_min = table.Column<int>(type: "integer", nullable: true),
                    NouvellesDonneesHisto = table.Column<bool>(type: "boolean", nullable: false),
                    NouvellesDonneesLit = table.Column<bool>(type: "boolean", nullable: false),
                    NumCh = table.Column<long>(type: "bigint", nullable: true),
                    Posture = table.Column<int>(type: "integer", nullable: true),
                    TempsMaxAllongeJour = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trackers",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccVector = table.Column<double[]>(type: "double precision[]", nullable: true),
                    CapteurId = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LecturesWifi = table.Column<string>(type: "jsonb", nullable: true),
                    NbPas = table.Column<long>(type: "bigint", nullable: false),
                    Power = table.Column<double>(type: "double precision", nullable: false, defaultValueSql: "0"),
                    Traite = table.Column<bool>(type: "boolean", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alarmes",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Acquittement = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CapteurId = table.Column<long>(type: "bigint", nullable: true),
                    Confirmation = table.Column<bool>(type: "boolean", nullable: true),
                    Creation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    NbNotifications = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: true),
                    Priorite = table.Column<int>(type: "integer", nullable: false),
                    UtilisateurId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarmes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarmes_Capteurs_CapteurId",
                        column: x => x.CapteurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Capteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alarmes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alarmes_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jours",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateJour = table.Column<DateTime>(type: "date", nullable: false),
                    JourTraite = table.Column<bool>(type: "boolean", nullable: false),
                    NbPas = table.Column<int>(type: "integer", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    TempsAllongeTotal = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jours_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nuits",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateDebut = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateFin = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateNuit = table.Column<DateTime>(type: "date", nullable: false),
                    DetailSorties = table.Column<string>(type: "jsonb", nullable: true),
                    DureeReveilAuLit = table.Column<TimeSpan>(type: "interval", nullable: true),
                    DureeReveilHorsLit = table.Column<TimeSpan>(type: "interval", nullable: true),
                    DureeSommeil = table.Column<TimeSpan>(type: "interval", nullable: true),
                    FCMax = table.Column<int>(type: "integer", nullable: true),
                    FCMin = table.Column<int>(type: "integer", nullable: true),
                    FCMoy = table.Column<int>(type: "integer", nullable: true),
                    FRMax = table.Column<int>(type: "integer", nullable: true),
                    FRMin = table.Column<int>(type: "integer", nullable: true),
                    FRMoy = table.Column<int>(type: "integer", nullable: true),
                    NbReveils = table.Column<int>(type: "integer", nullable: true),
                    NbSorties = table.Column<int>(type: "integer", nullable: true),
                    NuitTraitee = table.Column<bool>(type: "boolean", nullable: false),
                    PatientCId = table.Column<long>(type: "bigint", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    ScoreNuit = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nuits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nuits_PatientsC_PatientCId",
                        column: x => x.PatientCId,
                        principalSchema: "mobaspace_data",
                        principalTable: "PatientsC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nuits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Observables",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Chambre = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "TimeStamp", nullable: false),
                    ObservableTraite = table.Column<bool>(type: "boolean", nullable: false),
                    PatientCId = table.Column<long>(type: "bigint", nullable: true),
                    PatId = table.Column<long>(type: "bigint", nullable: true),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    Valeurs = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observables_PatientsC_PatientCId",
                        column: x => x.PatientCId,
                        principalSchema: "mobaspace_data",
                        principalTable: "PatientsC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observables_Patients_PatId",
                        column: x => x.PatId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observables_TypeObservable_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "mobaspace_data",
                        principalTable: "TypeObservable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2Apis_PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                column: "PatientCId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsPatients_PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients",
                column: "PatientCId");

            migrationBuilder.CreateIndex(
                name: "IX_Alarmes_CapteurId",
                schema: "mobaspace_data",
                table: "Alarmes",
                column: "CapteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Alarmes_PatientId",
                schema: "mobaspace_data",
                table: "Alarmes",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Alarmes_UtilisateurId",
                schema: "mobaspace_data",
                table: "Alarmes",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "U_Date_J_Patient",
                schema: "mobaspace_data",
                table: "Jours",
                columns: new[] { "PatientId", "DateJour" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nuits_PatientCId",
                schema: "mobaspace_data",
                table: "Nuits",
                column: "PatientCId");

            migrationBuilder.CreateIndex(
                name: "U_Date_Patient",
                schema: "mobaspace_data",
                table: "Nuits",
                columns: new[] { "PatientId", "DateNuit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Observables_PatientCId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "PatientCId");

            migrationBuilder.CreateIndex(
                name: "IX_Observables_PatId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "PatId");

            migrationBuilder.CreateIndex(
                name: "IX_Observables_TypeId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_NumCh",
                schema: "mobaspace_data",
                table: "Patients",
                column: "NumCh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trackers_CapteurId",
                schema: "mobaspace_data",
                table: "Trackers",
                column: "CapteurId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmesC_Patients_PatientId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
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
                name: "FK_ContactsPatients_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients",
                column: "PatientCId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
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
                name: "FK_JoursC_Patients_PatientId",
                schema: "mobaspace_data",
                table: "JoursC",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NuitsC_Patients_PatientId",
                schema: "mobaspace_data",
                table: "NuitsC",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OAuth2Apis_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                column: "PatientCId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_ObservablesC_Patients_PatientId",
                schema: "mobaspace_data",
                table: "ObservablesC",
                column: "PatientId",
                principalSchema: "mobaspace_data",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
