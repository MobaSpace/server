using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class AddHistoriqueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Historique",
                schema: "mobaspace_data",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key_id = table.Column<long>(type: "bigint", nullable: false),
                    nonce = table.Column<byte[]>(nullable: true),
                    Date_heure = table.Column<DateTime>(nullable: false),
                    UtilisateurId = table.Column<string>(nullable: true),
                    NumCh = table.Column<int>(nullable: false),
                    Data = table.Column<byte[]>(nullable: true),
                    Traite = table.Column<bool>(nullable: false)
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

            #region CreateView

            migrationBuilder.Sql($"Create OR REPLACE View HistoriqueView AS" +
                                    " Select \"Id\"," +
                                    " \"Date_heure\"," +
                                    " \"UtilisateurId\"," +
                                    " \"NumCh\"," +
                                    " convert_from(" +
                                    " crypto_secretbox_open(" +
                                    " \"Data\"," +
                                    " \"nonce\"," +
                                    " \"key_id\")," +
                                    " 'utf8') AS \"Data\", " +
                                    " \"Traite\"" +
                                    " From mobaspace_data.\"Historique\"" +
                                    " Order By \"Id\" asc;");

            migrationBuilder.Sql($"Create OR REPLACE FUNCTION Historique_encrypt_insert() RETURNS trigger" +
                                  " language plpgsql AS" +
                                  " $$" +
                                  " DECLARE" +
                                  " new_nonce bytea = crypto_secretbox_noncegen();" +
                                  " Historique_id bigint;" +
                                  " BEGIN" +
                                  " IF new.\"Id\" IS null then" +
                                  " insert into mobaspace_data.\"Historique\"(nonce) values(new_nonce) returning \"Id\" into Historique_id;" +
                                  " ELSE" +
                                  " insert into mobaspace_data.\"Historique\"(\"Id\", nonce) values (new.\"Id\", new_nonce) returning \"Id\" into Historique_id;" +
                                  " END IF;" +
                                  " update mobaspace_data.\"Historique\"" +
                                  " set \"Data\" =  crypto_secretbox(" +
                                  " convert_to(new.\"Data\", 'utf8')," +
                                  " new_nonce," +
                                  " key_id)," +
                                  " \"Date_heure\" =  new.\"Date_heure\"," +
                                  " \"UtilisateurId\" = new.\"UtilisateurId\"," +
                                  " \"NumCh\" = new.\"NumCh\"," +
                                  " \"Traite\" = new.\"Traite\"" +
                                  " where \"Id\" = Historique_id;" +
                                  " Return new;" +
                                  " END;" +
                                  " $$;");

            migrationBuilder.Sql("CREATE TRIGGER Historique_encrypt_trigger_insert" +
                     " INSTEAD OF INSERT ON HistoriqueView" +
                     " FOR EACH ROW" +
                     " EXECUTE FUNCTION Historique_encrypt_insert(); ");

            migrationBuilder.Sql($"Create OR REPLACE FUNCTION Historique_encrypt_update() RETURNS trigger" +
                                  " language plpgsql AS" +
                                  " $$" +
                                  " DECLARE" +
                                  " current_nonce bytea;" +
                                  " Historique_id bigint = new.\"Id\";" +
                                  " BEGIN" +
                                  " Select nonce into current_nonce from mobaspace_data.\"Historique\" where \"Id\" = Historique_id;" +
                                  " update mobaspace_data.\"Historique\"" +
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
                                  " where \"Id\" = Historique_id;" +
                                  " Return new;" +
                                  " END;" +
                                  " $$;");

            migrationBuilder.Sql("CREATE TRIGGER Historique_encrypt_trigger_update" +
                                 " INSTEAD OF UPDATE ON HistoriqueView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Historique_encrypt_update(); ");

            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historique",
                schema: "mobaspace_data");
        }
    }
}
