using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class AddColumnObservable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            #region Observables

            migrationBuilder.Sql("DROP VIEW IF EXISTS ObservablesView;");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Observable_encrypt_insert();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Observable_encrypt_update();");

            #endregion

            migrationBuilder.AddColumn<string>(
                name: "UriPersonnel",
                schema: "mobaspace_data",
                table: "Observables",
                nullable: true);

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
                                 " \"ObservableTraite\" = new.\"ObservableTraite\"" +
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UriPersonnel",
                schema: "mobaspace_data",
                table: "Observables");
        }
    }
}
