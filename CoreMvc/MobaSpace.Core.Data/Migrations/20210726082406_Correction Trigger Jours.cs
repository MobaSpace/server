using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class CorrectionTriggerJours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region Jours

            migrationBuilder.Sql("DROP VIEW IF EXISTS JoursView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Jour_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Jour_encrypt_update();");


            migrationBuilder.Sql("Create OR REPLACE View JoursView AS" +
                                 " Select \"Id\", \"PatientId\", \"DateJour\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"TempsAllongeTotal\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"TempsAllongeTotal\"," +
                                 " \"JourTraite\", Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"NbPas\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"NbPas\"" +
                                 " From mobaspace_data.\"Jours\"" +
                                 " Order By \"Id\" asc; ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Jour_encrypt_insert() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " new_nonce bytea = crypto_secretbox_noncegen();" +
                                 " Jours_id bigint;" +
                                 " BEGIN" +
                                 " IF new.\"Id\" IS null then" +
                                 " insert into mobaspace_data.\"Jours\"(nonce, \"PatientId\") values(new_nonce, new.\"PatientId\") returning \"Id\" into Jours_id;" +
                                 " ELSE" +
                                 " insert into mobaspace_data.\"Jours\"(\"Id\", nonce, \"PatientId\") values (new.\"Id\", new_nonce, new.\"PatientId\") returning \"Id\" into Jours_id;" +
                                 " END IF;" +
                                 " update mobaspace_data.\"Jours\"" +
                                 " set \"NbPas\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"NbPas\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"TempsAllongeTotal\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"TempsAllongeTotal\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DateJour\" = new.\"DateJour\"," +
                                 " \"JourTraite\" = CASE WHEN(new.\"JourTraite\" IS NULL) THEN false ELSE new.\"JourTraite\" END" +
                                 " where \"Id\" = Jours_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Jour_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON JoursView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION jour_encrypt_insert(); ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Jour_encrypt_update() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " Current_nonce bytea;" +
                                 " Jours_id bigint = new.\"Id\";" +
                                 " BEGIN" +
                                 " SELECT \"nonce\" into Current_nonce FROM mobaspace_data.\"Jours\" WHERE \"Id\" = Jours_id;" +
                                 " update mobaspace_data.\"Jours\"" +
                                 " set \"NbPas\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"NbPas\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(OLD.\"NbPas\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"TempsAllongeTotal\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"TempsAllongeTotal\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"TempsAllongeTotal\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"PatientId\" = COALESCE(new.\"PatientId\", OLD.\"PatientId\")," +
                                 " \"DateJour\" = COALESCE(new.\"DateJour\", OLD.\"DateJour\")," +
                                 " \"JourTraite\" = COALESCE(new.\"JourTraite\", OLD.\"JourTraite\")" +
                                 " Where \"Id\" = Jours_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Jour_encrypt_trigger_update" +
                                " INSTEAD OF UPDATE ON JoursView" +
                                " FOR EACH ROW" +
                                " EXECUTE FUNCTION Jour_encrypt_update(); ");

            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
