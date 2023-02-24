using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Suppressiondeschampsdefréquences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            #region Drop Fonction et View

            #region Patients

            migrationBuilder.Sql("DROP VIEW IF EXISTS PatientsView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Patient_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Patient_encrypt_update();");

            #endregion

            #region Nuits

            migrationBuilder.Sql("DROP VIEW IF EXISTS NuitsView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Nuit_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Nuit_encrypt_update();");

            #endregion


            #endregion


            #region dropColumn

            migrationBuilder.DropColumn(
                name: "FreqCardiaqueMax_bpm",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FreqCardiaqueMin_bpm",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FreqRespMax_bpm",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FreqRespMin_bpm",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Historique",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NouvellesDonneesHisto",
                schema: "mobaspace_data",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FCMax",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "FCMin",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "FCMoy",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "FRMax",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "FRMin",
                schema: "mobaspace_data",
                table: "Nuits");

            migrationBuilder.DropColumn(
                name: "FRMoy",
                schema: "mobaspace_data",
                table: "Nuits");

            #endregion

            #region Mise en place view et fonction

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
                                 " \"NuitTraitee\"" +
                                 " From mobaspace_data.\"Nuits\"" +
                                 " Order By \"Id\" asc; ");

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Nuit_encrypt_insert() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " new_nonce bytea = crypto_secretbox_noncegen();" +
                                 " Nuits_id bigint;" +
                                 " BEGIN" +
                                 " If new.\"Id\" IS null then" +
                                 " insert into mobaspace_data.\"Nuits\"(nonce, \"PatientId\") values(new_nonce, new.\"PatientId\") returning \"Id\" into Nuits_id;" +
                                 " ELSE" +
                                 " insert into mobaspace_data.\"Nuits\"(\"Id\", nonce, \"PatientId\") values (new.\"Id\", new_nonce, new.\"PatientId\") returning \"Id\" into Nuits_id;" +
                                 " END IF;" +
                                 " update mobaspace_data.\"Nuits\"" +
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

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Nuit_encrypt_update() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " Current_nonce bytea;" +
                                 " Nuits_id bigint = new.\"Id\";" +
                                 " BEGIN" +
                                 " Select \"nonce\" into Current_nonce from mobaspace_data.\"Nuits\" where \"Id\" = Nuits_id;" +
                                 " update mobaspace_data.\"Nuits\"" +
                                 " set \"DateDebut\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DateDebut\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"DateDebut\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"DateFin\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DateFin\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"DateFin\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"ScoreNuit\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"ScoreNuit\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"ScoreNuit\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"DureeSommeil\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DureeSommeil\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"DureeSommeil\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"NbReveils\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"NbReveils\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"NbReveils\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"NbSorties\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"NbSorties\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"NbSorties\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"DureeReveilAuLit\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DureeReveilAuLit\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"DureeReveilAuLit\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"DureeReveilHorsLit\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DureeReveilHorsLit\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"DureeReveilHorsLit\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"DetailSorties\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"DetailSorties\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"DetailSorties\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                  " \"DateNuit\" = COALESCE(new.\"DateNuit\", old.\"DateNuit\")," +
                                 " \"NuitTraitee\" = COALESCE(new.\"NuitTraitee\", old.\"NuitTraitee\")" +
                                 " where \"Id\" = Nuits_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Nuit_encrypt_trigger_update" +
                                 " INSTEAD OF UPDATE ON NuitsView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Nuit_encrypt_update();");

            #endregion

            #endregion

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FreqCardiaqueMax_bpm",
                schema: "mobaspace_data",
                table: "Patients",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FreqCardiaqueMin_bpm",
                schema: "mobaspace_data",
                table: "Patients",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FreqRespMax_bpm",
                schema: "mobaspace_data",
                table: "Patients",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FreqRespMin_bpm",
                schema: "mobaspace_data",
                table: "Patients",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Historique",
                schema: "mobaspace_data",
                table: "Patients",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NouvellesDonneesHisto",
                schema: "mobaspace_data",
                table: "Patients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "FCMax",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FCMin",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FCMoy",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FRMax",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FRMin",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FRMoy",
                schema: "mobaspace_data",
                table: "Nuits",
                type: "bytea",
                nullable: true);
        }
    }
}
