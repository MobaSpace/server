using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mobaspace_data");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Creation = table.Column<DateTime>(nullable: false),
                    LastConnection = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Capteurs",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Marque = table.Column<string>(nullable: false),
                    Modele = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    NumSerie = table.Column<string>(nullable: true),
                    Identifiant = table.Column<string>(nullable: true),
                    Creation = table.Column<DateTime>(nullable: false),
                    Modification = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capteurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Chambre = table.Column<string>(nullable: false),
                    CheminPhoto = table.Column<string>(nullable: true),
                    Historique = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{\"bed_actions\": null, \"blood_press\": null, \"sleep\": null}"),
                    NouvellesDonneesHisto = table.Column<bool>(nullable: false, defaultValue: false),
                    DerniereDateTraitee = table.Column<DateTime>(nullable: true),
                    NouvellesDonneesBPM = table.Column<bool>(nullable: false, defaultValue:false),
                    DernierCoucher = table.Column<DateTime>(nullable: true),
                    NouvellesDonneesLit = table.Column<bool>(nullable: false, defaultValue: false),
                    DernierLever = table.Column<DateTime>(nullable: true),
                    FreqCardiaqueMin_bpm = table.Column<int>(nullable: true),
                    FreqCardiaqueMax_bpm = table.Column<int>(nullable: true),
                    FreqRespMin_bpm = table.Column<int>(nullable: true),
                    FreqRespMax_bpm = table.Column<int>(nullable: true),
                    TensionArtSystMin_mmHg = table.Column<int>(nullable: true),
                    TensionArtSystMax_mmHg = table.Column<int>(nullable: true),
                    TensionArtDiastMin_mmHg = table.Column<int>(nullable: true),
                    TensionArtDiastMax_mmHg = table.Column<int>(nullable: true),
                    Coucher_h = table.Column<int>(nullable: true),
                    Coucher_min = table.Column<int>(nullable: true),
                    Lever_h = table.Column<int>(nullable: true),
                    Lever_min = table.Column<int>(nullable: true),
                    DureeMaxHorsLit_min = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "mobaspace_data",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "mobaspace_data",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "mobaspace_data",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alarmes",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CapteurId = table.Column<long>(nullable: true),
                    PatientId = table.Column<long>(nullable: true),
                    UtilisateurId = table.Column<string>(nullable: true),
                    Priorite = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Creation = table.Column<DateTime>(nullable: false),
                    Acquittement = table.Column<DateTime>(nullable: true),
                    NbNotifications = table.Column<int>(nullable: false, defaultValue: 5)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alarmes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alarmes_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactsPatients",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(nullable: false),
                    UtilisateurId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsPatients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactsPatients_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactsPatients_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OAuth2Apis",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Provider = table.Column<string>(nullable: false),
                    AccessToken = table.Column<string>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    ApiUserId = table.Column<string>(nullable: false),
                    ClientId = table.Column<string>(nullable: false),
                    ClientSecret = table.Column<string>(nullable: false),
                    PatientId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuth2Apis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OAuth2Apis_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ApisCapteurs",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApiId = table.Column<long>(nullable: true),
                    CapteurId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApisCapteurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApisCapteurs_OAuth2Apis_ApiId",
                        column: x => x.ApiId,
                        principalSchema: "mobaspace_data",
                        principalTable: "OAuth2Apis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApisCapteurs_Capteurs_CapteurId",
                        column: x => x.CapteurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "Capteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ApisCapteurs_ApiId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_ApisCapteurs_CapteurId",
                schema: "mobaspace_data",
                table: "ApisCapteurs",
                column: "CapteurId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "mobaspace_data",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "mobaspace_data",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "mobaspace_data",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "mobaspace_data",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "mobaspace_data",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "mobaspace_data",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactsPatients_PatientId",
                schema: "mobaspace_data",
                table: "ContactsPatients",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsPatients_UtilisateurId",
                schema: "mobaspace_data",
                table: "ContactsPatients",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_OAuth2Apis_PatientId",
                schema: "mobaspace_data",
                table: "OAuth2Apis",
                column: "PatientId");

            migrationBuilder.Sql("GRANT ALL ON SCHEMA mobaspace_data TO mobaspace_adm");
            migrationBuilder.Sql("GRANT USAGE ON SCHEMA mobaspace_data TO mobaspace_app_usr");
            migrationBuilder.Sql("GRANT USAGE ON SCHEMA mobaspace_data TO mobaspace_web_usr");

            migrationBuilder.GrantOnTable("mobaspace_data", "Alarmes", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "Alarmes", "mobaspace_app_usr", "INSERT", "SELECT", "UPDATE");
            migrationBuilder.GrantOnTable("mobaspace_data", "Alarmes", "mobaspace_web_usr", "SELECT", "UPDATE");
           
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetRoleClaims", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetRoleClaims", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetRoles", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetRoles", "mobaspace_app_usr", "SELECT");
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetRoles", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUserClaims", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUserClaims", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUserLogins", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUserLogins", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUserRoles", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUserRoles", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUserTokens", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUserTokens", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUsers", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUsers", "mobaspace_app_usr", "SELECT");
            migrationBuilder.GrantOnTable("mobaspace_data", "AspNetUsers", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "OAuth2Apis", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "OAuth2Apis", "mobaspace_app_usr", "SELECT", "UPDATE");
            migrationBuilder.GrantOnTable("mobaspace_data", "OAuth2Apis", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "Capteurs", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "Capteurs", "mobaspace_app_usr", "SELECT");
            migrationBuilder.GrantOnTable("mobaspace_data", "Capteurs", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "Patients", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "Patients", "mobaspace_app_usr", "SELECT");
            migrationBuilder.GrantOnTable("mobaspace_data", "Patients", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");

            migrationBuilder.GrantOnTable("mobaspace_data", "ContactsPatients", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "ContactsPatients", "mobaspace_app_usr", "SELECT");
            migrationBuilder.GrantOnTable("mobaspace_data", "ContactsPatients", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");
            
            migrationBuilder.GrantOnTable("mobaspace_data", "ApisCapteurs", "mobaspace_adm", "ALL");
            migrationBuilder.GrantOnTable("mobaspace_data", "ApisCapteurs", "mobaspace_app_usr", "SELECT");
            migrationBuilder.GrantOnTable("mobaspace_data", "ApisCapteurs", "mobaspace_web_usr", "INSERT", "SELECT", "DELETE", "UPDATE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarmes",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "ApisCapteurs",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "ContactsPatients",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "OAuth2Apis",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "Capteurs",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "mobaspace_data");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "mobaspace_data");
        }
    }
}
