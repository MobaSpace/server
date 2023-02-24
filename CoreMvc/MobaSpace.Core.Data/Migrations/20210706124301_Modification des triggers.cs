using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Modificationdestriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region Observable

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Observable_encrypt_insert() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " new_nonce bytea = crypto_secretbox_noncegen(); " +
                                 " Observables_id bigint;" +
                                 " BEGIN" +
                                 " IF new.\"Id\" IS null then" +
                                 " insert into mobaspace_data.\"Observables\"(nonce, \"TypeObservableId\") values(new_nonce, new.\"TypeObservableId\") returning \"Id\" into Observables_id;" +
                                 " ELSE " +
                                 " insert into mobaspace_data.\"Observables\"(\"Id\", nonce, \"TypeObservableId\") values (new.\"Id\", new_nonce, new.\"TypeObservableId\") returning \"Id\" into Observables_id;" +
                                 " END IF;" +
                                 " update mobaspace_data.\"Observables\"" +
                                 " set \"Values\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"Valeurs\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"Chambre\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"Chambre\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"Date\" = new.\"Date\"," +
                                 " \"PatientId\" = new.\"PatientId\"," +
                                 " \"ObservableTraite\" = COALESCE(new.\"ObservableTraite\" , false)" +
                                 " where \"Id\" = Observables_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; "
                                 );

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Observable_encrypt_update() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " current_nonce bytea;" +
                                 " Observables_id bigint = new.\"Id\";" +
                                 " BEGIN" +
                                 " Select nonce into current_nonce from mobaspace_data.\"Observables\" where \"Id\" = Observables_id;" +
                                 " update mobaspace_data.\"Observables\"" +
                                 " set \"Values\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"Valeurs\" As text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"Valeurs\" As text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"Chambre\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"Chambre\" As text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"Chambre\" As text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"Date\" = COALESCE(new.\"Date\", old.\"Date\")," +
                                 " \"PatientId\" = COALESCE(new.\"PatientId\", old.\"PatientId\")," +
                                 " \"ObservableTraite\" = COALESCE(new.\"ObservableTraite\", old.\"ObservableTraite\", false)" +
                                 " where \"Id\" = Observables_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 "$$; ");

            #endregion

            #region Patient

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
                                 " \"CheminPhoto\" = new.\"CheminPhoto\"," +
                                 " \"NouvellesDonneesLit\" = CASE WHEN(new.\"NouvellesDonneesLit\" IS NULL) THEN false ELSE new.\"NouvellesDonneesLit\" END," +
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

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Patient_encrypt_update() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " current_nonce bytea;" +
                                 " Patients_id bigint = new.\"Id\";" +
                                 " BEGIN" +
                                 " Select \"nonce\" into current_nonce from mobaspace_data.\"Patients\" WHERE \"Id\" = Patients_id;" +
                                 " update mobaspace_data.\"Patients\"" +
                                 " set \"Chambre\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(new.\"Chambre\", 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(old.\"Chambre\", 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"DernierCoucher\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DernierCoucher\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"DernierCoucher\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"DernierLever\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DernierLever\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"DernierLever\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"CumulTempsAllonge\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"CumulTempsAllonge\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"CumulTempsAllonge\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"NumCh\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"NumCh\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"NumCh\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"CheminPhoto\" = COALESCE(new.\"CheminPhoto\", old.\"CheminPhoto\")," +
                                 " \"NouvellesDonneesLit\" = COALESCE(new.\"NouvellesDonneesLit\", old.\"NouvellesDonneesLit\")," +
                                 " \"Coucher_h\" = COALESCE(new.\"Coucher_h\", old.\"Coucher_h\")," +
                                 " \"Coucher_min\" = COALESCE(new.\"Coucher_min\", old.\"Coucher_min\")," +
                                 " \"Lever_h\" = COALESCE(new.\"Lever_h\", old.\"Lever_h\")," +
                                 " \"Lever_min\" = COALESCE(new.\"Lever_min\", old.\"Lever_min\")," +
                                 " \"DureeMaxHorsLit_min\" = COALESCE(new.\"DureeMaxHorsLit_min\", old.\"DureeMaxHorsLit_min\")," +
                                 " \"Posture\" = COALESCE(new.\"Posture\", old.\"Posture\")," +
                                 " \"TempsMaxAllongeJour\" = COALESCE(new.\"TempsMaxAllongeJour\", old.\"TempsMaxAllongeJour\")" +
                                 " where \"Id\" = Patients_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");


            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
