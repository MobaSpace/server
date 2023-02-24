using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class FixErrorHistorique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region DropView


            migrationBuilder.Sql("DROP VIEW IF EXISTS HistoriqueView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Historique_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Historique_encrypt_update();");

            #endregion

            migrationBuilder.AlterColumn<long>(
                name: "key_id",
                schema: "mobaspace_data",
                table: "Historique",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint");

            #region view

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
            migrationBuilder.AlterColumn<long>(
                name: "key_id",
                schema: "mobaspace_data",
                table: "Historique",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);
        }
    }
}
