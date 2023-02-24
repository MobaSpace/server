using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class FixUpdatePatient3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region Patients

            migrationBuilder.Sql("DROP VIEW IF EXISTS PatientsView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Patient_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Patient_encrypt_update();");

            #endregion

            #region Patient
            migrationBuilder.Sql($"Create OR REPLACE View PatientsView AS" +
                                   " SELECT \"Id\"," +
                                   " convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"Chambre\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') AS \"Chambre\"" +
                                   " , \"CheminPhoto\"," +
                                   " convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"DernierCoucher\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8')::timestamp AS \"DernierCoucher\"," +
                                   " \"NouvellesDonneesLit\"," +
                                   " convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"DernierLever\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8')::timestamp AS \"DernierLever\"," +
                                   " \"Coucher_h\", \"Coucher_min\", \"Lever_h\", \"Lever_min\", \"DureeMaxHorsLit_min\", \"Posture\", \"TempsMaxAllongeJour\"," +
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"CumulTempsAllonge\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') as integer) AS \"CumulTempsAllonge\"," +
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"DerniereLocalisation\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') as jsonb) AS \"DerniereLocalisation\"," +
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"NumCh\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') as bigint) AS \"NumCh\"" +
                                   " From mobaspace_data.\"Patients\"" +
                                   " Order By \"Id\" asc;");

            migrationBuilder.Sql($"CREATE OR REPLACE FUNCTION Patient_encrypt_insert() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " new_nonce bytea = crypto_secretbox_noncegen();" +
                                 " Patients_id bigint;" +
                                 " BEGIN" +
                                 " IF new.\"Id\" IS null then" +
                                 " insert into mobaspace_data.\"Patients\"(nonce) values(new_nonce) returning \"Id\" into Patients_id;" +
                                 " ELSE" +
                                 " insert into mobaspace_data.\"Patients\"(\"Id\", nonce) values (new.\"Id\", new_nonce) returning \"Id\" into Patients_id;" +
                                 " END IF;" +
                                 " update mobaspace_data.\"Patients\"" +
                                 " set \"Chambre\" = crypto_secretbox(" +
                                 " convert_to(new.\"Chambre\", 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DernierCoucher\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DernierCoucher\" AS text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DernierLever\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DernierLever\" AS text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"CumulTempsAllonge\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"CumulTempsAllonge\" AS text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"NumCh\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"NumCh\" AS text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DerniereLocalisation\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DerniereLocalisation\" AS text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"CheminPhoto\" = new.\"CheminPhoto\"," +
                                 " \"Coucher_h\" = new.\"Coucher_h\"," +
                                 " \"Coucher_min\" = new.\"Coucher_min\"," +
                                 " \"Lever_h\" = new.\"Lever_h\"," +
                                 " \"Lever_min\" = new.\"Lever_min\"," +
                                 " \"DureeMaxHorsLit_min\" = new.\"DureeMaxHorsLit_min\" ," +
                                 " \"Posture\" = new.\"Posture\"," +
                                 " \"TempsMaxAllongeJour\" = new.\"TempsMaxAllongeJour\"" +
                                 " where \"Id\" = Patients_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Patient_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON PatientsView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Patient_encrypt_insert(); ");


            migrationBuilder.Sql(" CREATE OR REPLACE FUNCTION Patient_encrypt_update() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " current_nonce bytea;" +
                                 " Patients_id bigint = new.\"Id\";" +
                                 " BEGIN" +
                                 " Select \"nonce\" into current_nonce from mobaspace_data.\"Patients\" WHERE \"Id\" = Patients_id;" +
                                 " update mobaspace_data.\"Patients\"" +
                                 " set \"Chambre\" = crypto_secretbox(" +
                                 " convert_to(new.\"Chambre\", 'utf8')," +
                                 " current_nonce," +
                                 " key_id)," +
                                 " \"DernierCoucher\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DernierCoucher\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id)," +
                                 " \"DernierLever\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DernierLever\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id)," +
                                 " \"CumulTempsAllonge\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"CumulTempsAllonge\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id)," +
                                 " \"DerniereLocalisation\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DerniereLocalisation\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id)," +
                                 " \"NumCh\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"NumCh\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id)," +
                                 " \"CheminPhoto\" = new.\"CheminPhoto\"," +
                                 " \"NouvellesDonneesLit\" = new.\"NouvellesDonneesLit\"," +
                                 " \"Coucher_h\" = new.\"Coucher_h\"," +
                                 " \"Coucher_min\" = new.\"Coucher_min\"," +
                                 " \"Lever_h\" = new.\"Lever_h\"," +
                                 " \"Lever_min\" = new.\"Lever_min\"," +
                                 " \"DureeMaxHorsLit_min\" = new.\"DureeMaxHorsLit_min\"," +
                                 " \"Posture\" = new.\"Posture\"," +
                                 " \"TempsMaxAllongeJour\" = new.\"TempsMaxAllongeJour\"" +
                                 " where \"Id\" = Patients_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Patient_encrypt_trigger_update" +
                                 " INSTEAD OF UPDATE ON PatientsView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Patient_encrypt_update(); ");


            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
