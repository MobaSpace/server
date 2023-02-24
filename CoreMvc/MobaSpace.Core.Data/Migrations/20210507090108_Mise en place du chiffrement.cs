using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Miseenplaceduchiffrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<double>(
                name: "Power",
                schema: "mobaspace_data",
                table: "TrackersC",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double),
                oldType: "double precision");


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
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"Historique\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8')AS jsonb) AS \"Historique\"," +
                                   " \"NouvellesDonneesHisto\"," +
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
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"FreqCardiaqueMin_bpm\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') as integer)AS \"FreqCardiaqueMin_bpm\"," +
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"FreqCardiaqueMax_bpm\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') as integer) AS \"FreqCardiaqueMax_bpm\"," +
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"FreqRespMax_bpm\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') as integer) AS \"FreqRespMax_bpm\"," +
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"FreqRespMin_bpm\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') as integer) AS \"FreqRespMin_bpm\"," +
                                   " \"Coucher_h\", \"Coucher_min\", \"Lever_h\", \"Lever_min\", \"DureeMaxHorsLit_min\", \"Posture\", \"DernierIndiceTraite\", \"TempsMaxAllongeJour\"," +
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"CumulTempsAllonge\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') as integer) AS \"CumulTempsAllonge\"," +
                                   " Cast(convert_from(" +
                                   " crypto_secretbox_open(" +
                                   " \"NumCh\"," +
                                   " \"nonce\"," +
                                   " \"key_id\")," +
                                   " 'utf8') as bigint) AS \"NumCh\"" +
                                   " From mobaspace_data.\"PatientsC\"" +
                                   " Order By \"Id\" asc;");

            migrationBuilder.Sql($"CREATE OR REPLACE FUNCTION Patient_encrypt_insert() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " new_nonce bytea = crypto_secretbox_noncegen();" +
                                 " Patients_id bigint;" +
                                 " BEGIN" +
                                 " IF new.\"Id\" IS null then"+
                                 " insert into mobaspace_data.\"PatientsC\"(nonce) values(new_nonce) returning \"Id\" into Patients_id;" +
                                 " ELSE"+
                                 " insert into mobaspace_data.\"PatientsC\"(\"Id\", nonce) values (new.\"Id\", new_nonce) returning \"Id\" into Patients_id;"+
                                 " END IF;"+
                                 " update mobaspace_data.\"PatientsC\"" +
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
                                 " \"Historique\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"Historique\" AS text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FreqCardiaqueMax_bpm\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FreqCardiaqueMax_bpm\" AS text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FreqCardiaqueMin_bpm\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FreqCardiaqueMin_bpm\" AS text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FreqRespMax_bpm\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FreqRespMax_bpm\" AS text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FreqRespMin_bpm\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FreqRespMin_bpm\" AS text), 'utf8')," +
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
                                 " \"NouvellesDonneesHisto\" = CASE WHEN(new.\"NouvellesDonneesHisto\" IS NULL)THEN false ELSE new.\"NouvellesDonneesHisto\" END," +
                                 " \"DernierIndiceTraite\" = new.\"DernierIndiceTraite\"," +
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

            migrationBuilder.Sql("CREATE TRIGGER Patient_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON PatientsView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Patient_encrypt_insert(); ");


            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Patient_encrypt_update() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " current_nonce bytea;" +
                                 " Patients_id bigint = new.\"Id\";" +
                                 " BEGIN" +
                                 " Select nonce into current_nonce from mobaspace_data.\"PatientsC\";" +
                                 " update mobaspace_data.\"PatientsC\"" +
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
                                 " \"Historique\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"Historique\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"Historique\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"FreqCardiaqueMax_bpm\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FreqCardiaqueMax_bpm\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"FreqCardiaqueMax_bpm\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"FreqCardiaqueMin_bpm\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FreqCardiaqueMin_bpm\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"FreqCardiaqueMin_bpm\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"FreqRespMax_bpm\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FreqRespMax_bpm\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"FreqRespMax_bpm\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"FreqRespMin_bpm\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FreqRespMin_bpm\" AS text), 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"FreqRespMin_bpm\" AS text), 'utf8')," +
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
                                 " \"NouvellesDonneesHisto\" = COALESCE(new.\"NouvellesDonneesHisto\", old.\"NouvellesDonneesHisto\")," +
                                 " \"DernierIndiceTraite\" = COALESCE(new.\"DernierIndiceTraite\", old.\"DernierIndiceTraite\")," +
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

            migrationBuilder.Sql("CREATE TRIGGER Patient_encrypt_trigger_update" +
                                 " INSTEAD OF UPDATE ON PatientsView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Patient_encrypt_update(); ");


            #endregion

            #region Alarme
            migrationBuilder.Sql("Create OR REPLACE View AlarmesView AS" +
                             " Select \"Id\", \"CapteurId\", \"PatientId\", \"UtilisateurId\", \"Priorite\"," +
                             " convert_from(" +
                             " crypto_secretbox_open(" +
                             " \"Description\"," +
                             " \"nonce\"," +
                             " \"key_id\")," +
                             " 'utf8') AS \"Description\"," +
                             " \"Creation\", \"Acquittement\", \"NbNotifications\", \"Confirmation\"" +
                             " From mobaspace_data.\"AlarmesC\"" +
                             " Order By \"Id\" asc; ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Alarme_encrypt_insert() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " new_nonce bytea = crypto_secretbox_noncegen();" +
                                 " Alarmes_id bigint;" +
                                 " BEGIN" +
                                 " IF new.\"Id\" IS null then"+
                                 " insert into mobaspace_data.\"AlarmesC\"(nonce) values(new_nonce) returning \"Id\" into Alarmes_id;" +
                                 " ELSE"+
                                 " insert into mobaspace_data.\"AlarmesC\"( \"Id\", nonce) values (new.\"Id\", new_nonce) returning \"Id\" into Alarmes_id;"+
                                 " END IF;"+
                                 " update mobaspace_data.\"AlarmesC\"" +
                                 " set  \"Description\" = crypto_secretbox(" +
                                 " convert_to(new.\"Description\", 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"CapteurId\" = new.\"CapteurId\"," +
                                 " \"PatientId\" = new.\"PatientId\"," +
                                 " \"UtilisateurId\" = new.\"UtilisateurId\"," +
                                 " \"Priorite\" = new.\"Priorite\"," +
                                 " \"Creation\" = new.\"Creation\"," +
                                 " \"Acquittement\" = new.\"Acquittement\"," +
                                 " \"NbNotifications\" = new.\"NbNotifications\"," +
                                 " \"Confirmation\" = new.\"Confirmation\"" +
                                 " where \"Id\" = Alarmes_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Alarme_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON AlarmesView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Alarme_encrypt_insert(); ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Alarme_encrypt_update() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " current_nonce bytea;" +
                                 " Alarmes_id bigint = new.\"Id\";" +
                                 " BEGIN" +
                                 " Select nonce into current_nonce from mobaspace_data.\"AlarmesC\" where \"Id\" = Alarmes_id;" +
                                 " update mobaspace_data.\"AlarmesC\"" +
                                 " set \"Description\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(new.\"Description\", 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(old.\"Description\", 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"CapteurId\" = COALESCE(new.\"CapteurId\", old.\"CapteurId\")," +
                                 " \"PatientId\" = COALESCE(new.\"PatientId\", old.\"PatientId\")," +
                                 " \"UtilisateurId\" = COALESCE(new.\"UtilisateurId\", old.\"UtilisateurId\")," +
                                 " \"Priorite\" = COALESCE(new.\"Priorite\", old.\"Priorite\")," +
                                 " \"Creation\" = COALESCE(new.\"Creation\", old.\"Creation\")," +
                                 " \"Acquittement\" = COALESCE(new.\"Acquittement\", old.\"Acquittement\")," +
                                 " \"NbNotifications\" = COALESCE(new.\"NbNotifications\", old.\"NbNotifications\")," +
                                 " \"Confirmation\" = COALESCE(new.\"Confirmation\", old.\"Confirmation\")" +
                                 " where \"Id\" = Alarmes_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Alarme_encrypt_trigger_update" +
                                 " INSTEAD OF UPDATE ON AlarmesView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Alarme_encrypt_update(); ");



            #endregion

            #region Jour

            migrationBuilder.Sql("Create OR REPLACE View JoursView AS"+
                                 " Select \"Id\", \"PatientId\", \"DateJour\","+
                                 " Cast(convert_from("+
                                 " crypto_secretbox_open("+
                                 " \"TempsAllongeTotal\","+
                                 " \"nonce\","+
                                 " \"key_id\"),"+
                                 " 'utf8') AS integer) AS \"TempsAllongeTotal\","+
                                 " \"JourTraite\", Cast(convert_from("+
                                 " crypto_secretbox_open("+
                                 " \"NbPas\","+
                                 " \"nonce\","+
                                 " \"key_id\"),"+
                                 " 'utf8') AS integer) AS \"NbPas\""+
                                 " From mobaspace_data.\"JoursC\""+
                                 " Order By \"Id\" asc; ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Jour_encrypt_insert() RETURNS trigger"+
                                 " language plpgsql AS"+
                                 " $$"+
                                 " DECLARE"+
                                 " new_nonce bytea = crypto_secretbox_noncegen();"+
                                 " Jours_id bigint;"+
                                 " BEGIN"+
                                 " IF new.\"Id\" IS null then"+
                                 " insert into mobaspace_data.\"JoursC\"(nonce, \"PatientId\") values(new_nonce, new.\"PatientId\") returning \"Id\" into Jours_id;" +
                                 " ELSE"+
                                 " insert into mobaspace_data.\"JoursC\"(\"Id\", nonce, \"PatientId\") values (new.\"Id\", new_nonce, new.\"PatientId\") returning \"Id\" into Jours_id;"+
                                 " END IF;"+
                                 " update mobaspace_data.\"JoursC\"" +
                                 " set \"NbPas\" = crypto_secretbox("+
                                 " convert_to(Cast(new.\"NbPas\" As text), 'utf8'),"+
                                 " new_nonce,"+
                                 " key_id),"+
		                         " \"TempsAllongeTotal\" = crypto_secretbox("+
                                 " convert_to(Cast(new.\"TempsAllongeTotal\" As text), 'utf8'),"+
                                 " new_nonce,"+
                                 " key_id),"+
		                         " \"DateJour\" = new.\"DateJour\","+
		                         " \"JourTraite\" = CASE WHEN(new.\"JourTraite\" IS NULL) THEN false ELSE new.\"JourTraite\" END"+
                                 " where \"Id\" = Jours_id;"+
                                 " RETURN new;"+
                                 " END;"+ 	
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Jour_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON JoursView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION jour_encrypt_insert(); ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Jour_encrypt_update() RETURNS trigger"+
                                 " language plpgsql AS"+
                                 " $$"+
                                 " DECLARE"+
                                 " Current_nonce bytea;"+
                                 " Jours_id bigint = new.\"Id\";"+
                                 " BEGIN"+
                                 " SELECT \"nonce\" into Current_nonce FROM mobaspace_data.\"JoursC\" WHERE \"Id\" = Jours_id;"+
                                 " update mobaspace_data.\"JoursC\""+
                                 " set \"NbPas\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"NbPas\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(OLD.\"NbPas\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"TempsAllongeTotal\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"TempsAllongeTotal\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"TempsAllongeTotal\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"PatientId\" = COALESCE(new.\"PatientId\", OLD.\"PatientId\"),"+
		                         " \"DateJour\" = COALESCE(new.\"DateJour\", OLD.\"DateJour\"),"+
		                         " \"JourTraite\" = COALESCE(new.\"JourTraite\", OLD.\"JourTraite\")"+
                                 " Where \"Id\" = Jours_id;"+
                                 " RETURN new;"+
                                 " END;"+
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Jour_encrypt_trigger_update" +
                                " INSTEAD OF INSERT ON JoursView" +
                                " FOR EACH ROW" +
                                " EXECUTE FUNCTION Jour_encrypt_update(); ");

            #endregion

            #region Nuit

            migrationBuilder.Sql("Create OR REPLACE View NuitsView AS" +
                                 " Select \"Id\", \"PatientId\", \"DateNuit\"," +
                                 " convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"DateDebut\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8')::timestamp AS \"DateDebut\"," +
                                 " convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"DateFin\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8')::timestamp AS \"DateFin\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"ScoreNuit\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"ScoreNuit\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"DureeSommeil\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS interval) AS \"DureeSommeil\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"NbReveils\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"NbReveils\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"NbSorties\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"NbSorties\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"DureeReveilAuLit\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS interval) AS \"DureeReveilAuLit\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"DureeReveilHorsLit\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS interval) AS \"DureeReveilHorsLit\"," +
                                 " Cast(convert_from(" +
                                  " crypto_secretbox_open(" +
                                 " \"DetailSorties\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS jsonb) AS \"DetailSorties\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"FCMin\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"FCMin\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"FCMoy\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"FCMoy\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"FCMax\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"FCMax\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"FRMin\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"FRMin\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"FRMoy\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"FRMoy\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"FRMax\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"FRMax\"," +
                                 " \"NuitTraitee\"" +
                                 " From mobaspace_data.\"NuitsC\"" +
                                 " Order By \"Id\" asc; ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Nuit_encrypt_insert() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " new_nonce bytea = crypto_secretbox_noncegen();" +
                                 " Nuits_id bigint;" +
                                 " BEGIN" +
                                 " If new.\"Id\" IS null then"+
                                 " insert into mobaspace_data.\"NuitsC\"(nonce, \"PatientId\") values(new_nonce, new.\"PatientId\") returning \"Id\" into Nuits_id;" +
                                 " ELSE"+
                                 " insert into mobaspace_data.\"NuitsC\"(\"Id\", nonce, \"PatientId\") values (new.\"Id\", new_nonce, new.\"PatientId\") returning \"Id\" into Nuits_id;" +
                                 " END IF;"+
                                 " update mobaspace_data.\"NuitsC\"" +
                                 " set \"DateDebut\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DateDebut\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DateFin\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DateFin\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"ScoreNuit\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"ScoreNuit\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DureeSommeil\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DureeSommeil\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"NbReveils\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"NbReveils\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"NbSorties\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"NbSorties\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DureeReveilAuLit\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DureeReveilAuLit\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DureeReveilHorsLit\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DureeReveilHorsLit\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DetailSorties\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DetailSorties\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FCMin\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FCMin\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FCMoy\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FCMoy\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FCMax\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FCMax\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FRMin\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FRMin\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FRMoy\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FRMoy\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"FRMax\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"FRMax\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"DateNuit\" = new.\"DateNuit\"," +
                                 " \"NuitTraitee\" = CASE WHEN(new.\"NuitTraitee\" IS NULL) THEN false ELSE new.\"NuitTraitee\" END" +
                                 " where \"Id\" = Nuits_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");



            migrationBuilder.Sql("CREATE TRIGGER Nuit_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON NuitsView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Nuit_encrypt_insert(); ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Nuit_encrypt_update() RETURNS trigger"+
                                 " language plpgsql AS"+
                                 " $$"+
                                 " DECLARE"+
                                 " Current_nonce bytea;"+
                                 " Nuits_id bigint = new.\"Id\";"+
                                 " BEGIN"+
                                 " Select \"nonce\" into Current_nonce from mobaspace_data.\"NuitsC\" where \"Id\" = Nuits_id;"+
                                 " update mobaspace_data.\"NuitsC\""+
                                 " set \"DateDebut\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"DateDebut\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"DateDebut\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
                                 " \"DateFin\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"DateFin\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"DateFin\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"ScoreNuit\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"ScoreNuit\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"ScoreNuit\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"DureeSommeil\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"DureeSommeil\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"DureeSommeil\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"NbReveils\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"NbReveils\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"NbReveils\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"NbSorties\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"NbSorties\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"NbSorties\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"DureeReveilAuLit\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"DureeReveilAuLit\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"DureeReveilAuLit\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"DureeReveilHorsLit\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"DureeReveilHorsLit\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"DureeReveilHorsLit\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"DetailSorties\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"DetailSorties\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"DetailSorties\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"FCMin\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"FCMin\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"FCMin\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"FCMoy\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"FCMoy\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"FCMoy\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"FCMax\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"FCMax\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"FCMax\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"FRMin\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"FRMin\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"FRMin\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"FRMoy\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"FRMoy\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"FRMoy\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"FRMax\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"FRMax\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"FRMax\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
	                         	 " \"DateNuit\" = COALESCE(new.\"DateNuit\", old.\"DateNuit\"),"+
		                         " \"NuitTraitee\" = COALESCE(new.\"NuitTraitee\", old.\"NuitTraitee\")"+
                                 " where \"Id\" = Nuits_id;"+
                                 " RETURN new;"+
                                 " END;"+
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Nuit_encrypt_trigger_update"+
                                 " INSTEAD OF UPDATE ON NuitsView"+
                                 " FOR EACH ROW"+
                                 " EXECUTE FUNCTION Nuit_encrypt_update();");
 
            #endregion

            #region Observable
            migrationBuilder.Sql("Create OR REPLACE View ObservablesView AS" +
                                 " SELECT \"Id\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"Values\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS jsonb) AS \"Valeurs\"," +
                                 " \"Date\", \"PatientId\", \"TypeObservableId\", \"ObservableTraite\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"Chambre\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"Chambre\"" +
                                 " From mobaspace_data.\"ObservablesC\"" +
                                 " ORDER By \"Id\" asc;"
            );

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Observable_encrypt_insert() RETURNS trigger"+
                                 " language plpgsql AS"+
                                 " $$"+
                                 " DECLARE"+
                                 " new_nonce bytea = crypto_secretbox_noncegen(); "+
                                 " Observables_id bigint;"+
                                 " BEGIN"+
                                 " IF new.\"Id\" IS null then"+
                                 " insert into mobaspace_data.\"ObservablesC\"(nonce, \"TypeObservableId\") values(new_nonce, new.\"TypeObservableId\") returning \"Id\" into Observables_id;"+
                                 " ELSE "+
                                 " insert into mobaspace_data.\"ObservablesC\"(\"Id\", nonce, \"TypeObservableId\") values (new.\"Id\", new_nonce, new.\"TypeObservableId\") returning \"Id\" into Observables_id;"+
                                 " END IF;"+
                                 " update mobaspace_data.\"ObservablesC\""+
                                 " set \"Values\" = crypto_secretbox("+
                                 " convert_to(Cast(new.\"Valeurs\" As text), 'utf8'),"+
                                 " new_nonce,"+
                                 " key_id),"+
		                         " \"Chambre\" = crypto_secretbox("+
                                 " convert_to(Cast(new.\"Chambre\" As text), 'utf8'),"+
                                 " new_nonce,"+
                                 " key_id),"+
		                         " \"Date\" = new.\"Date\","+
		                         " \"PatientId\" = new.\"PatientId\","+
		                         " \"ObservableTraite\" = new.\"ObservableTraite\""+
                                 " where \"Id\" = Observables_id;"+
                                 " RETURN new;"+
                                 " END;"+
                                 " $$; "
                                 );

            migrationBuilder.Sql("CREATE TRIGGER Observable_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON ObservablesView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Observable_encrypt_insert(); ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Observable_encrypt_update() RETURNS trigger"+
                                 " language plpgsql AS"+
                                 " $$"+
                                 " DECLARE"+
                                 " current_nonce bytea;"+
                                 " Observables_id bigint = new.\"Id\";"+
                                 " BEGIN"+
                                 " Select nonce into current_nonce from mobaspace_data.\"ObservablesC\" where \"Id\" = Observables_id;"+
                                 " update mobaspace_data.\"ObservablesC\""+
                                 " set \"Values\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"Valeurs\" As text), 'utf8'),"+
                                 " current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"Valeurs\" As text), 'utf8'),"+
                                 " current_nonce,"+
                                 " key_id)),"+
		                         " \"Chambre\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"Chambre\" As text), 'utf8'),"+
                                 " current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(old.\"Chambre\" As text), 'utf8'),"+
                                 " current_nonce,"+
                                 " key_id)),"+
		                         " \"Date\" = COALESCE(new.\"Date\", old.\"Date\"),"+
	                         	 " \"PatientId\" = COALESCE(new.\"PatientId\", old.\"PatientId\"),"+
		                         " \"ObservableTraite\" = COALESCE(new.\"ObservableTraite\", old.\"ObservableTraite\")"+
                                 " where \"Id\" = Observables_id;"+
                                 " RETURN new;"+
                                 " END;"+
                                 "$$; ");

            migrationBuilder.Sql("CREATE TRIGGER Observable_encrypt_trigger_update" +
                                 " INSTEAD OF UPDATE ON ObservablesView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Observable_encrypt_update(); ");

            #endregion

            #region Tracker

            migrationBuilder.Sql("Create OR REPLACE View TrackersView AS"+
                                 " Select \"Id\", \"LecturesWifi\","+
                                 " Cast(convert_from("+
                                 " crypto_secretbox_open("+
                                 " \"NbPas\","+
                                 " \"nonce\","+
                                 " \"key_id\"),"+
                                 " 'utf8') AS bigint) AS \"NbPas\","+
                                 " \"AccVector\", \"LastUpdate\", \"CapteurId\", \"Power\", \"Traite\""+
                                 " From mobaspace_data.\"TrackersC\""+
                                 " Order By \"Id\" asc; ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Tracker_encrypt_insert() RETURNS trigger"+
                                 " language plpgsql AS"+
                                 " $$"+
                                 " DECLARE"+
                                 " new_nonce bytea = crypto_secretbox_noncegen();"+
                                 " Trackers_id bigint;"+
                                 " BEGIN"+
                                 " IF new.\"Id\" IS null then"+
                                 " insert into mobaspace_data.\"TrackersC\"(nonce, \"CapteurId\", \"LastUpdate\", \"Power\", \"Traite\") values(new_nonce, new.\"CapteurId\", new.\"LastUpdate\", new.\"Power\", new.\"Traite\") returning \"Id\" into Trackers_id;"+
                                 " ELSE"+
                                 " insert into mobaspace_data.\"TrackersC\"(\"Id\", nonce , \"CapteurId\", \"LastUpdate\", \"Power\" , \"Traite\" ) values (new.\"Id\", new_nonce, new.\"CapteurId\", new.\"LastUpdate\", new.\"Power\", new.\"Traite\") returning \"Id\" into Trackers_id;"+
                                 " END IF;"+
                                 " update mobaspace_data.\"TrackersC\"" +
                                 " set \"NbPas\" = crypto_secretbox("+
                                 " convert_to(Cast(new.\"NbPas\" As text), 'utf8'),"+
                                 " new_nonce,"+
                                 " key_id),"+
		                         " \"AccVector\" = new.\"AccVector\","+
		                         " \"LecturesWifi\" = new.\"LecturesWifi\","+
		                         " \"LastUpdate\" = new.\"LastUpdate\","+
		                         " \"Power\" = new.\"Power\","+
		                         " \"Traite\" = new.\"Traite\""+
                                 " where \"Id\" = Trackers_id;"+
                                 " RETURN new;"+
                                 " END;"+ 	
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Tracker_encrypt_trigger_insert"+
                                 " INSTEAD OF INSERT ON TrackersView"+
                                 " FOR EACH ROW"+
                                 " EXECUTE FUNCTION Tracker_encrypt_insert(); ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Tracker_encrypt_update() RETURNS trigger"+
                                 " language plpgsql AS"+
                                 " $$"+
                                 " DECLARE"+
                                 " Current_nonce bytea;"+
                                 " Trackers_id bigint = new.\"Id\";"+
                                 " BEGIN"+
                                 " SELECT \"nonce\" into Current_nonce FROM mobaspace_data.\"TrackersC\" WHERE \"Id\" = Trackers_id;"+
                                 " update mobaspace_data.\"TrackersC\""+
                                 " set \"NbPas\" = COALESCE(crypto_secretbox("+
                                 " convert_to(Cast(new.\"NbPas\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id), crypto_secretbox("+
                                 " convert_to(Cast(OLD.\"NbPas\" As text), 'utf8'),"+
                                 " Current_nonce,"+
                                 " key_id)),"+
		                         " \"AccVector\" = COALESCE(new.\"AccVector\", OLD.\"AccVector\"),"+
		                         " \"LecturesWifi\" = COALESCE(new.\"LecturesWifi\", OLD.\"LecturesWifi\"),"+
		                         " \"LastUpdate\" = COALESCE(new.\"LastUpdate\", OLD.\"LastUpdate\"),"+
		                         " \"Power\" = COALESCE(new.\"Power\", OLD.\"Power\"),"+
		                         " \"Traite\" = COALESCE(new.\"Traite\", OLD.\"Traite\")"+
                                 " Where \"Id\" = Trackers_id;"+
                                 " RETURN new;"+
                                 " END;"+
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Tracker_encrypt_trigger_update"+
                                 " INSTEAD OF UPDATE ON TrackersView"+
                                 " FOR EACH ROW"+
                                 " EXECUTE FUNCTION Tracker_encrypt_update();");


            #endregion

            #region Chiffrement

            migrationBuilder.Sql("INSERT INTO JoursView(\"Id\", \"PatientId\", \"DateJour\", \"TempsAllongeTotal\", \"NbPas\", \"JourTraite\")" +
                                 " SELECT \"Id\", \"PatientId\", \"DateJour\", \"TempsAllongeTotal\", \"NbPas\", \"JourTraite\"" +
                                 " From mobaspace_data.\"Jours\";");

            migrationBuilder.Sql("INSERT INTO TrackersView(\"Id\", \"LecturesWifi\", \"NbPas\", \"AccVector\", \"LastUpdate\", \"CapteurId\", \"Power\", \"Traite\")" +
                                 " SELECT \"Id\", \"LecturesWifi\", \"NbPas\", \"AccVector\", \"LastUpdate\", \"CapteurId\", \"Power\" ,\"Traite\"" +
                                 " From mobaspace_data.\"Trackers\";");

            migrationBuilder.Sql("INSERT INTO ObservablesView(\"Id\", \"Valeurs\", \"Date\", \"PatientId\", \"TypeObservableId\", \"ObservableTraite\", \"Chambre\") " +
                                 " SELECT \"Id\", \"Valeurs\", \"Date\", \"PatId\", \"TypeId\", \"ObservableTraite\", \"Chambre\" " +
                                 " From mobaspace_data.\"Observables\";");

            migrationBuilder.Sql("INSERT INTO NuitsView(\"Id\", \"PatientId\", \"DateNuit\", \"DateDebut\", \"DateFin\", \"ScoreNuit\", \"DureeSommeil\", \"NbReveils\", \"NbSorties\", \"DureeReveilAuLit\", \"DureeReveilHorsLit\", \"DetailSorties\", \"FCMin\", \"FCMoy\", \"FCMax\", \"FRMin\", \"FRMoy\", \"FRMax\", \"NuitTraitee\") " +
                                 " SELECT \"Id\", \"PatientId\", \"DateNuit\", \"DateDebut\", \"DateFin\", \"ScoreNuit\", \"DureeSommeil\", \"NbReveils\", \"NbSorties\", \"DureeReveilAuLit\", \"DureeReveilHorsLit\", \"DetailSorties\", \"FCMin\", \"FCMoy\", \"FCMax\", \"FRMin\", \"FRMoy\", \"FRMax\", \"NuitTraitee\" " +
                                 " From mobaspace_data.\"Nuits\";");

            migrationBuilder.Sql("INSERT INTO PatientsView(\"Id\", \"Chambre\", \"CheminPhoto\", \"Historique\", \"NouvellesDonneesHisto\", \"DernierCoucher\", \"NouvellesDonneesLit\", \"DernierLever\", \"FreqCardiaqueMin_bpm\", \"FreqCardiaqueMax_bpm\" , \"FreqRespMax_bpm\", \"FreqRespMin_bpm\", \"Coucher_h\", \"Coucher_min\", \"Lever_h\", \"Lever_min\", \"DureeMaxHorsLit_min\", \"Posture\", \"DernierIndiceTraite\", \"TempsMaxAllongeJour\", \"CumulTempsAllonge\", \"NumCh\"  ) " +
                                 " SELECT \"Id\", \"Chambre\", \"CheminPhoto\", \"Historique\", \"NouvellesDonneesHisto\", \"DernierCoucher\", \"NouvellesDonneesLit\", \"DernierLever\", \"FreqCardiaqueMin_bpm\", \"FreqCardiaqueMax_bpm\", \"FreqRespMax_bpm\", \"FreqRespMin_bpm\", \"Coucher_h\", \"Coucher_min\", \"Lever_h\", \"Lever_min\", \"DureeMaxHorsLit_min\", \"Posture\", \"DernierIndiceTraite\", \"TempsMaxAllongeJour\", \"CumulTempsAllonge\", \"NumCh\" " +
                                 " From mobaspace_data.\"Patients\";");

            migrationBuilder.Sql("INSERT INTO AlarmesView(\"Id\",\"CapteurId\", \"PatientId\", \"UtilisateurId\", \"Priorite\", \"Description\", \"Creation\", \"Acquittement\", \"NbNotifications\", \"Confirmation\")" +
                                 " SELECT \"Id\", \"CapteurId\", \"PatientId\", \"UtilisateurId\", \"Priorite\", \"Description\", \"Creation\", \"Acquittement\", \"NbNotifications\", \"Confirmation\" " +
                                 " From mobaspace_data.\"Alarmes\";");

            #endregion

            #region Refresh Id

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"ObservablesC\"', 'Id')," +
                                " (SELECT MAX(\"Id\") FROM mobaspace_data.\"ObservablesC\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"TrackersC\"', 'Id')," +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"TrackersC\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"AlarmesC\"', 'Id'), " +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"AlarmesC\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"PatientsC\"', 'Id')," +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"PatientsC\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"NuitsC\"', 'Id')," +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"NuitsC\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"JoursC\"', 'Id')," +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"JoursC\"));");


            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<double>(
            name: "Power",
            schema: "mobaspace_data",
            table: "TrackersC",
            type: "double precision",
            nullable: false,
            oldClrType: typeof(double),
            oldDefaultValueSql: "0");

        }
    }
}
