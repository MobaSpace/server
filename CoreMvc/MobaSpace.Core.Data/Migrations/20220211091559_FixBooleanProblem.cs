using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class FixBooleanProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region DropView


            migrationBuilder.Sql("DROP VIEW IF EXISTS InfoBlocChambreView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS InfoBlocChambre_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS InfoBlocChambre_encrypt_update();");


            migrationBuilder.Sql("DROP VIEW IF EXISTS ObservablesView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Observable_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Observable_encrypt_update();");

            #endregion

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
                                  " set \"Data\" = crypto_secretbox(" +
                                  " convert_to(Cast(new.\"Data\" AS text), 'utf8')," +
                                  " new_nonce," +
                                  " key_id)," +
                                  " \"Date\" = new.\"Date\"," +
                                  " \"UtilisateurId\" = new.\"UtilisateurId\"," +
                                  " \"NumCh\" = new.\"NumCh\"," +
                                  " \"Traite\" = CASE WHEN(new.\"Traite\" IS NULL) THEN false ELSE new.\"Traite\" END" +
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

            #region Observable
            migrationBuilder.Sql("Create OR REPLACE View ObservablesView AS" +
                                 " SELECT \"Id\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"Values\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS jsonb) AS \"Valeurs\"," +
                                 " \"Date\", \"PatientId\", \"TypeObservableId\", \"ObservableTraite\", \"UriPersonnel\"," +
                                 " Cast(convert_from(" +
                                 " crypto_secretbox_open(" +
                                 " \"Chambre\"," +
                                 " \"nonce\"," +
                                 " \"key_id\")," +
                                 " 'utf8') AS integer) AS \"Chambre\"" +
                                 " From mobaspace_data.\"Observables\"" +
                                 " ORDER By \"Id\" asc;"
            );

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
                                 " \"TypeObservableId\" = new.\"TypeObservableId\"," +
                                 " \"PatientId\" = new.\"PatientId\"," +
                                 " \"UriPersonnel\" = new.\"UriPersonnel\"," +
                                 " \"ObservableTraite\" = CASE WHEN(new.\"ObservableTraite\" IS NULL) THEN false ELSE new.\"ObservableTraite\" END" +
                                 " where \"Id\" = Observables_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; "
                                 );

            migrationBuilder.Sql("CREATE TRIGGER Observable_encrypt_trigger_insert" +
                                 " INSTEAD OF INSERT ON ObservablesView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Observable_encrypt_insert(); ");

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
                                 " \"TypeObservableId\" = new.\"TypeObservableId\"," +
                                 " \"PatientId\" = COALESCE(new.\"PatientId\", old.\"PatientId\")," +
                                 " \"ObservableTraite\" = COALESCE(new.\"ObservableTraite\", old.\"ObservableTraite\")," +
                                 " \"UriPersonnel\" =  COALESCE(new.\"UriPersonnel\",  old.\"UriPersonnel\")" +
                                 " where \"Id\" = Observables_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 "$$; ");

            migrationBuilder.Sql("CREATE TRIGGER Observable_encrypt_trigger_update" +
                                 " INSTEAD OF UPDATE ON ObservablesView" +
                                 " FOR EACH ROW" +
                                 " EXECUTE FUNCTION Observable_encrypt_update(); ");

            #endregion

            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
