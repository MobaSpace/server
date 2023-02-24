using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Fix2InfoBlocChambreView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region DropView


            migrationBuilder.Sql("DROP VIEW IF EXISTS InfoBlocChambreView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS InfoBlocChambre_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS InfoBlocChambre_encrypt_update();");

            #endregion

            migrationBuilder.AlterColumn<bool>(
                name: "Traite",
                schema: "mobaspace_data",
                table: "InfoBlocChambre",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "false");

            #region view

            migrationBuilder.Sql($"Create OR REPLACE View InfoBlocChambreView AS" +
                                    " Select \"Id\"," +
                                    " \"Date\"," +
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
                                  " convert_to(Cast(new.\"Data\" AS text), 'utf8')," +
                                  " new_nonce," +
                                  " key_id)," +
                                  " \"Date\" =  new.\"Date\"," +
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
                                  " convert_to(Cast(new.\"Data\" AS text), 'utf8')," +
                                  " current_nonce," +
                                  " key_id), " +
                                  " crypto_secretbox(" +
                                  " convert_to(Cast(old.\"Data\" AS text), 'utf8')," +
                                  " current_nonce," +
                                  " key_id) " +
                                  " )," +
                                  " \"Date\" =  COALESCE(new.\"Date\", old.\"Date\")," +
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
            migrationBuilder.AlterColumn<bool>(
                name: "Traite",
                schema: "mobaspace_data",
                table: "InfoBlocChambre",
                type: "boolean",
                nullable: false,
                defaultValueSql: "false",
                oldClrType: typeof(bool),
                oldDefaultValue: false);
        }
    }
}
