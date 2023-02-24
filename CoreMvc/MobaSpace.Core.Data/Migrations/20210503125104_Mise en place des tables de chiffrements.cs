using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Miseenplacedestablesdechiffrements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "Observables",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "Nuits",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlarmesC",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    nonce = table.Column<byte[]>(nullable: true),
                    CapteurId = table.Column<long>(nullable: true),
                    PatientId = table.Column<long>(nullable: true),
                    UtilisateurId = table.Column<string>(nullable: true),
                    Priorite = table.Column<int>(nullable: false, defaultValue: 1),
                    Description = table.Column<byte[]>(nullable: true),
                    Creation = table.Column<DateTime>(nullable: false, defaultValueSql: "Now()"),
                    Acquittement = table.Column<DateTime>(nullable: true),
                    NbNotifications = table.Column<int>(nullable: false, defaultValue: 5),
                    Confirmation = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmesC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmesC_Capteurs_CapteurId",
                        column: x => x.CapteurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Capteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmesC_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmesC_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JoursC",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    nonce = table.Column<byte[]>(nullable: true),
                    PatientId = table.Column<long>(nullable: false),
                    DateJour = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "NOW()"),
                    TempsAllongeTotal = table.Column<byte[]>(nullable: true),
                    NbPas = table.Column<byte[]>(nullable: true),
                    JourTraite = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoursC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoursC_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NuitsC",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    nonce = table.Column<byte[]>(nullable: true),
                    DateNuit = table.Column<DateTime>(nullable: false, defaultValueSql: "NOW()"),
                    PatientId = table.Column<long>(nullable: false),
                    DateDebut = table.Column<byte[]>(nullable: true),
                    DateFin = table.Column<byte[]>(nullable: true),
                    ScoreNuit = table.Column<byte[]>(nullable: true),
                    DureeSommeil = table.Column<byte[]>(nullable: true),
                    NbReveils = table.Column<byte[]>(nullable: true),
                    NbSorties = table.Column<byte[]>(nullable: true),
                    DureeReveilAuLit = table.Column<byte[]>(nullable: true),
                    DureeReveilHorsLit = table.Column<byte[]>(nullable: true),
                    DetailSorties = table.Column<byte[]>(nullable: true),
                    FCMin = table.Column<byte[]>(nullable: true),
                    FCMoy = table.Column<byte[]>(nullable: true),
                    FCMax = table.Column<byte[]>(nullable: true),
                    FRMin = table.Column<byte[]>(nullable: true),
                    FRMoy = table.Column<byte[]>(nullable: true),
                    FRMax = table.Column<byte[]>(nullable: true),
                    NuitTraitee = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuitsC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NuitsC_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservablesC",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    nonce = table.Column<byte[]>(nullable: true),
                    Values = table.Column<byte[]>(nullable: true),
                    TypeObservableId = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "NOW()"),
                    Chambre = table.Column<byte[]>(nullable: true),
                    ObservableTraite = table.Column<bool>(nullable: false, defaultValue: false),
                    PatientId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservablesC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservablesC_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObservablesC_TypeObservable_TypeObservableId",
                        column: x => x.TypeObservableId,
                        principalSchema: "mobaspace_data",
                        principalTable: "TypeObservable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientsC",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    nonce = table.Column<byte[]>(nullable: true),
                    Chambre = table.Column<byte[]>(nullable: true),
                    NumCh = table.Column<byte[]>(nullable: true),
                    CheminPhoto = table.Column<string>(nullable: true),
                    Historique = table.Column<byte[]>(nullable: true),
                    NouvellesDonneesHisto = table.Column<bool>(nullable: false, defaultValue: false),
                    DernierIndiceTraite = table.Column<long>(nullable: true),
                    DernierCoucher = table.Column<byte[]>(nullable: true),
                    NouvellesDonneesLit = table.Column<bool>(nullable: false, defaultValue: false),
                    DernierLever = table.Column<byte[]>(nullable: true),
                    FreqCardiaqueMin_bpm = table.Column<byte[]>(nullable: true),
                    FreqCardiaqueMax_bpm = table.Column<byte[]>(nullable: true),
                    FreqRespMin_bpm = table.Column<byte[]>(nullable: true),
                    FreqRespMax_bpm = table.Column<byte[]>(nullable: true),
                    Coucher_h = table.Column<int>(nullable: true),
                    Coucher_min = table.Column<int>(nullable: true),
                    Lever_h = table.Column<int>(nullable: true),
                    Lever_min = table.Column<int>(nullable: true),
                    DureeMaxHorsLit_min = table.Column<int>(nullable: true),
                    Posture = table.Column<int>(nullable: true),
                    TempsMaxAllongeJour = table.Column<int>(nullable: true),
                    CumulTempsAllonge = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsC", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Observables_PatientCId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "PatientCId");

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2Apis_PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                column: "PatientCId");

            migrationBuilder.CreateIndex(
                name: "IX_Nuits_PatientCId",
                schema: "mobaspace_data",
                table: "Nuits",
                column: "PatientCId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsPatients_PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients",
                column: "PatientCId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmesC_CapteurId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                column: "CapteurId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmesC_PatientId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmesC_UtilisateurId",
                schema: "mobaspace_data",
                table: "AlarmesC",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "U_Date_J_PatientC",
                schema: "mobaspace_data",
                table: "JoursC",
                columns: new[] { "PatientId", "DateJour" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Date_PatientC",
                schema: "mobaspace_data",
                table: "NuitsC",
                columns: new[] { "PatientId", "DateNuit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ObservablesC_PatientId",
                schema: "mobaspace_data",
                table: "ObservablesC",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ObservablesC_TypeObservableId",
                schema: "mobaspace_data",
                table: "ObservablesC",
                column: "TypeObservableId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsC_NumCh",
                schema: "mobaspace_data",
                table: "PatientsC",
                column: "NumCh",
                unique: true);

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
                name: "FK_Nuits_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "Nuits",
                column: "PatientCId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Observables_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "Observables",
                column: "PatientCId",
                principalSchema: "mobaspace_data",
                principalTable: "PatientsC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactsPatients_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_Nuits_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropForeignKey(
                name: "FK_OAuth2Apis_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis");

            migrationBuilder.DropForeignKey(
                name: "FK_Observables_PatientsC_PatientCId",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropTable(
                name: "AlarmesC",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "JoursC",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "NuitsC",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "ObservablesC",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "PatientsC",
                schema: "mobaspace_data");

            migrationBuilder.DropIndex(
                name: "IX_Observables_PatientCId",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropIndex(
                name: "IX_OAuth2Apis_PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis");

            migrationBuilder.DropIndex(
                name: "IX_Nuits_PatientCId",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropIndex(
                name: "IX_ContactsPatients_PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients");

            migrationBuilder.DropColumn(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "Observables");

            migrationBuilder.DropColumn(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "OAuth2Apis");

            migrationBuilder.DropColumn(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "PatientCId",
                schema: "mobaspace_data",
                table: "ContactsPatients");
        }
    }
}
