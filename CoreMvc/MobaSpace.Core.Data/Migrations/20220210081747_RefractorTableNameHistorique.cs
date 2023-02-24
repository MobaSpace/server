using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class RefractorTableNameHistorique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            #region DropView


            migrationBuilder.Sql("DROP VIEW IF EXISTS HistoriqueView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Historique_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Historique_encrypt_update();");

            #endregion

            migrationBuilder.DropTable(
                name: "Historique",
                schema: "mobaspace_data");

            migrationBuilder.CreateTable(
                name: "InfoBlocChambre",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    nonce = table.Column<byte[]>(nullable: true),
                    Date_heure = table.Column<DateTime>(nullable: true),
                    UtilisateurId = table.Column<string>(nullable: true),
                    NumCh = table.Column<int>(nullable: true),
                    Data = table.Column<byte[]>(nullable: true),
                    Traite = table.Column<bool>(nullable: false, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoBlocChambre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoBlocChambre_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfoBlocChambre_UtilisateurId",
                schema: "mobaspace_data",
                table: "InfoBlocChambre",
                column: "UtilisateurId");

            #region view

            migrationBuilder.Sql($"Create OR REPLACE View InfoBlocChambreView AS" +
                                    " Select \"Id\"," +
                                    " \"Date_heure\"," +
                                    " \"UtilisateurId\"," +
                                    " \"NumCh\"," +
                                    " Cast(convert_from(" +
                                    " crypto_secretbox_open(" +
                                    " \"Data\"," +
                                    " \"nonce\"," +
                                    " \"key_id\")," +
                                    " 'utf8') AS jsonb) AS \"Data\", " +
                                    " \"Traite\"" +
                                    " From mobaspace_data.\"InfoBlocChambre\"" +
                                    " Order By \"Id\" asc;");

            migrationBuilder.Sql($"Create OR REPLACE FUNCTION InfoBlocChambre_encrypt_insert() RETURNS trigger" +
                                  " language plpgsql AS" +
                                  " $$" +
                                  " DECLARE" +
                                  " new_nonce bytea = crypto_secretbox_noncegen();" +
                                  " InfoBlocChambre_id bigint;" +
                                  " BEGIN" +
                                  " IF new.\"Id\" IS null then" +
                                  " insert into mobaspace_data.\"InfoBlocChambre\"(nonce) values(new_nonce) returning \"Id\" into InfoBlocChambre_id;" +
                                  " ELSE" +
                                  " insert into mobaspace_data.\"InfoBlocChambre\"(\"Id\", nonce) values (new.\"Id\", new_nonce) returning \"Id\" into InfoBlocChambre_id;" +
                                  " END IF;" +
                                  " update mobaspace_data.\"InfoBlocChambre\"" +
                                  " set \"Data\" =  crypto_secretbox(" +
                                  " convert_to(new.\"Data\", 'utf8')," +
                                  " new_nonce," +
                                  " key_id)," +
                                  " \"Date_heure\" =  new.\"Date_heure\"," +
                                  " \"UtilisateurId\" = new.\"UtilisateurId\"," +
                                  " \"NumCh\" = new.\"NumCh\"," +
                                  " \"Traite\" = new.\"Traite\"" +
                                  " where \"Id\" = InfoBlocChambre_id;" +
                                  " Return new;" +
                                  " END;" +
                                  " $$;");

            migrationBuilder.Sql("CREATE TRIGGER InfoBlocChambre_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON InfoBlocChambreView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION InfoBlocChambre_encrypt_insert(); ");

            migrationBuilder.Sql($"Create OR REPLACE FUNCTION InfoBlocChambre_encrypt_update() RETURNS trigger" +
                                  " language plpgsql AS" +
                                  " $$" +
                                  " DECLARE" +
                                  " current_nonce bytea;" +
                                  " InfoBlocChambre_id bigint = new.\"Id\";" +
                                  " BEGIN" +
                                  " Select nonce into current_nonce from mobaspace_data.\"InfoBlocChambre\" where \"Id\" = InfoBlocChambre_id;" +
                                  " update mobaspace_data.\"InfoBlocChambre\"" +
                                  " set \"Data\" = COALESCE( crypto_secretbox(" +
                                  " convert_to(new.\"Data\", 'utf8')," +
                                  " current_nonce," +
                                  " key_id), " +
                                  " crypto_secretbox(" +
                                  " convert_to(old.\"Data\", 'utf8')," +
                                  " current_nonce," +
                                  " key_id) " +
                                  " )," +
                                  " \"Date_heure\" =  COALESCE(new.\"Date_heure\", old.\"Date_heure\")," +
                                  " \"UtilisateurId\" = COALESCE(new.\"UtilisateurId\", old.\"UtilisateurId\")," +
                                  " \"NumCh\" =  COALESCE(new.\"NumCh\", old.\"NumCh\")," +
                                  " \"Traite\" = COALESCE(new.\"Traite\", old.\"Traite\")" +
                                  " where \"Id\" = InfoBlocChambre_id;" +
                                  " Return new;" +
                                  " END;" +
                                  " $$;");

            migrationBuilder.Sql("CREATE TRIGGER InfoBlocChambre_encrypt_trigger_update" +
                                 " INSTEAD OF UPDATE ON InfoBlocChambreView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION InfoBlocChambre_encrypt_update(); ");

            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoBlocChambre",
                schema: "mobaspace_data");

            migrationBuilder.CreateTable(
                name: "Historique",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<byte[]>(type: "bytea", nullable: true),
                    Date_heure = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NumCh = table.Column<int>(type: "integer", nullable: true),
                    Traite = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    UtilisateurId = table.Column<string>(type: "text", nullable: true),
                    key_id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    nonce = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historique", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historique_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalSchema: "mobaspace_data",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historique_UtilisateurId",
                schema: "mobaspace_data",
                table: "Historique",
                column: "UtilisateurId");
        }
    }
}
