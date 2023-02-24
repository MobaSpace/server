using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Modificationdestriggertrackers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                                 " \"AccVector\" = new.\"AccVector\"," +
                                 " \"LecturesWifi\" = new.\"LecturesWifi\"," +
                                 " \"LastUpdate\" = new.\"LastUpdate\"," +
                                 " \"Power\" = new.\"Power\"," +
                                 " \"Traite\" = COALESCE(new.\"Traite\", false)" +
                                 " where \"Id\" = Trackers_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
