using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Modificationdestablesjoursettrackers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region DeleteView
            #region Jour
            migrationBuilder.Sql("DROP VIEW IF EXISTS JoursView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Jour_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Jour_encrypt_update();");

            #endregion
            #region Tracker

            migrationBuilder.Sql("DROP VIEW IF EXISTS TrackersView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Tracker_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Tracker_encrypt_update();");

            #endregion
            #endregion
            migrationBuilder.AddColumn<byte[]>(
                name: "ActivityTime",
                schema: "mobaspace_data",
                table: "Trackers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "VitesseMarche",
                schema: "mobaspace_data",
                table: "Trackers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TempsTotalActivite",
                schema: "mobaspace_data",
                table: "Jours",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "VitesseMarcheMoyenne",
                schema: "mobaspace_data",
                table: "Jours",
                nullable: true);

            #region Creation de la view et ses trigers

            #region Jour

            #region View
            migrationBuilder.Sql("Create OR REPLACE View JoursView AS" +
                                 " Select \"Id\", \"PatientId\", \"DateJour\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"VitesseMarcheMoyenne\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS double precision[]) AS \"VitesseMarcheMoyenne\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"TempsTotalActivite\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"TempsTotalActivite\"," +
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
            #endregion

            #region Insert

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
                                 " \"VitesseMarcheMoyenne\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"VitesseMarcheMoyenne\" As text), 'utf8')," +
                                 " new_nonce," +
                                 " key_id)," +
                                 " \"TempsTotalActivite\" = crypto_secretbox(" +
                                 " convert_to(Cast(new.\"TempsTotalActivite\" As text), 'utf8')," +
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
            #endregion

            #region Update

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
                                 " \"VitesseMarcheMoyenne\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"VitesseMarcheMoyenne\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"VitesseMarcheMoyenne\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id))," +
                                 " \"TempsTotalActivite\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(Cast(new.\"TempsTotalActivite\" As text), 'utf8')," +
                                 " Current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(Cast(old.\"TempsTotalActivite\" As text), 'utf8')," +
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

            #endregion

            #region Trackers

            #region View 
            migrationBuilder.Sql("Create OR REPLACE View TrackersView AS" +
                           " Select \"Id\", \"LecturesWifi\"," +
                           " Cast(convert_from(" +
                           " crypto_secretbox_open(" +
                           " \"NbPas\"," +
                           " \"nonce\"," +
                           " \"key_id\")," +
                           " 'utf8') AS bigint) AS \"NbPas\"," +
                           " \"AccVector\", \"LastUpdate\",+" +
                           " Cast(convert_from(" +
                           " crypto_secretbox_open(" +
                           " \"VitesseMarche\"," +
                           " \"nonce\"," +
                           " \"key_id\")," +
                           " 'utf8') AS double precision) AS \"VitesseMarche\"," +
                           " Cast(convert_from(" +
                           " crypto_secretbox_open(" +
                           " \"ActivityTime\"," +
                           " \"nonce\"," +
                           " \"key_id\")," +
                           " 'utf8') AS integer) AS \"ActivityTime\"," +
                           " \"CapteurId\", \"Power\", \"Traite\"" +
                           " From mobaspace_data.\"Trackers\"" +
                           " Order By \"Id\" asc; ");
            #endregion

            #region Insert

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Tracker_encrypt_insert() RETURNS trigger" +
                              " language plpgsql AS" +
                              " $$" +
                              " DECLARE" +
                              " new_nonce bytea = crypto_secretbox_noncegen();" +
                              " Trackers_id bigint;" +
                              " BEGIN" +
                              " IF new.\"Id\" IS null then" +
                              " insert into mobaspace_data.\"Trackers\"(nonce, \"CapteurId\", \"LastUpdate\", \"Power\", \"Traite\") values(new_nonce, new.\"CapteurId\", new.\"LastUpdate\", new.\"Power\", new.\"Traite\") returning \"Id\" into Trackers_id;" +
                              " ELSE" +
                              " insert into mobaspace_data.\"Trackers\"(\"Id\", nonce , \"CapteurId\", \"LastUpdate\", \"Power\" , \"Traite\" ) values (new.\"Id\", new_nonce, new.\"CapteurId\", new.\"LastUpdate\", new.\"Power\", new.\"Traite\") returning \"Id\" into Trackers_id;" +
                              " END IF;" +
                              " update mobaspace_data.\"Trackers\"" +
                              " set \"NbPas\" = crypto_secretbox(" +
                              " convert_to(Cast(new.\"NbPas\" As text), 'utf8')," +
                              " new_nonce," +
                              " key_id)," +
                              " \"VitesseMarche\" = crypto_secretbox(" +
                              " convert_to(Cast(new.\"VitesseMarche\" As text), 'utf8')," +
                              " new_nonce," +
                              " key_id)," +
                              " \"ActivityTime\" = crypto_secretbox(" +
                              " convert_to(Cast(new.\"ActivityTime\" As text), 'utf8')," +
                              " new_nonce," +
                              " key_id)," +
                              " \"AccVector\" = new.\"AccVector\"," +
                              " \"LecturesWifi\" = new.\"LecturesWifi\"," +
                              " \"LastUpdate\" = new.\"LastUpdate\"," +
                              " \"Power\" = new.\"Power\"," +
                              " \"Traite\" = COALESCE(new.\"Traite\", false)" +
                              " where \"Id\" = Trackers_id;" +
                              " RETURN new;" +
                              " END;" +
                              " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Tracker_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON TrackersView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Tracker_encrypt_insert(); ");


            #endregion

            #region Update

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Tracker_encrypt_update() RETURNS trigger" +
                                " language plpgsql AS" +
                                " $$" +
                                " DECLARE" +
                                " Current_nonce bytea;" +
                                " Trackers_id bigint = new.\"Id\";" +
                                " BEGIN" +
                                " SELECT \"nonce\" into Current_nonce FROM mobaspace_data.\"Trackers\" WHERE \"Id\" = Trackers_id;" +
                                " update mobaspace_data.\"Trackers\"" +
                                " set \"NbPas\" = COALESCE(crypto_secretbox(" +
                                " convert_to(Cast(new.\"NbPas\" As text), 'utf8')," +
                                " Current_nonce," +
                                " key_id), crypto_secretbox(" +
                                " convert_to(Cast(OLD.\"NbPas\" As text), 'utf8')," +
                                " Current_nonce," +
                                " key_id))," +
                                " \"VitesseMarche\" = COALESCE(crypto_secretbox(" +
                                " convert_to(Cast(new.\"VitesseMarche\" As text), 'utf8')," +
                                " Current_nonce," +
                                " key_id), crypto_secretbox(" +
                                " convert_to(Cast(OLD.\"VitesseMarche\" As text), 'utf8')," +
                                " Current_nonce," +
                                " key_id))," +
                                " \"ActivityTime\" = COALESCE(crypto_secretbox(" +
                                " convert_to(Cast(new.\"ActivityTime\" As text), 'utf8')," +
                                " Current_nonce," +
                                " key_id), crypto_secretbox(" +
                                " convert_to(Cast(OLD.\"ActivityTime\" As text), 'utf8')," +
                                " Current_nonce," +
                                " key_id))," +
                                " \"AccVector\" = COALESCE(new.\"AccVector\", OLD.\"AccVector\")," +
                                " \"LecturesWifi\" = COALESCE(new.\"LecturesWifi\", OLD.\"LecturesWifi\")," +
                                " \"LastUpdate\" = COALESCE(new.\"LastUpdate\", OLD.\"LastUpdate\")," +
                                " \"Power\" = COALESCE(new.\"Power\", OLD.\"Power\")," +
                                " \"Traite\" = COALESCE(new.\"Traite\", OLD.\"Traite\")" +
                                " Where \"Id\" = Trackers_id;" +
                                " RETURN new;" +
                                " END;" +
                                " $$; ");

            migrationBuilder.Sql("CREATE TRIGGER Tracker_encrypt_trigger_update" +
                                 " INSTEAD OF UPDATE ON TrackersView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Tracker_encrypt_update();");

            #endregion

            #endregion

            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityTime",
                schema: "mobaspace_data",
                table: "Trackers");

            migrationBuilder.DropColumn(
                name: "VitesseMarche",
                schema: "mobaspace_data",
                table: "Trackers");

            migrationBuilder.DropColumn(
                name: "TempsTotalActivite",
                schema: "mobaspace_data",
                table: "Jours");

            migrationBuilder.DropColumn(
                name: "VitesseMarcheMoyenne",
                schema: "mobaspace_data",
                table: "Jours");
        }
    }
}
