using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class ModificationdutriggerUpdatedelatabletrackers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                                " \"AccVector\" = new.\"AccVector\"," +
                                " \"LecturesWifi\" = new.\"LecturesWifi\"," +
                                " \"LastUpdate\" = COALESCE(new.\"LastUpdate\", OLD.\"LastUpdate\")," +
                                " \"Power\" = COALESCE(new.\"Power\", OLD.\"Power\")," +
                                " \"Traite\" = COALESCE(new.\"Traite\", OLD.\"Traite\")" +
                                " Where \"Id\" = Trackers_id;" +
                                " RETURN new;" +
                                " END;" +
                                " $$; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
