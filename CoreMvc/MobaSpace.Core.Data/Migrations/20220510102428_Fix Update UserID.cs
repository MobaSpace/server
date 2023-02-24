using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class FixUpdateUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Alarme_encrypt_update() RETURNS trigger" +
                                 " language plpgsql AS" +
                                 " $$" +
                                 " DECLARE" +
                                 " current_nonce bytea;" +
                                 " Alarmes_id bigint = new.\"Id\";" +
                                 " BEGIN" +
                                 " Select nonce into current_nonce from mobaspace_data.\"Alarmes\" where \"Id\" = Alarmes_id;" +
                                 " update mobaspace_data.\"Alarmes\"" +
                                 " set \"Description\" = COALESCE(crypto_secretbox(" +
                                 " convert_to(new.\"Description\", 'utf8')," +
                                 " current_nonce," +
                                 " key_id), crypto_secretbox(" +
                                 " convert_to(old.\"Description\", 'utf8')," +
                                 " current_nonce," +
                                 " key_id))," +
                                 " \"CapteurId\" = COALESCE(new.\"CapteurId\", old.\"CapteurId\")," +
                                 " \"PatientId\" = COALESCE(new.\"PatientId\", old.\"PatientId\")," +
                                 " \"UtilisateurId\" = new.\"UtilisateurId\"," +
                                 " \"Priorite\" = COALESCE(new.\"Priorite\", old.\"Priorite\")," +
                                 " \"Creation\" = COALESCE(new.\"Creation\", old.\"Creation\")," +
                                 " \"Acquittement\" = COALESCE(new.\"Acquittement\", old.\"Acquittement\")," +
                                 " \"NbNotifications\" = COALESCE(new.\"NbNotifications\", old.\"NbNotifications\")," +
                                 " \"Confirmation\" = COALESCE(new.\"Confirmation\", old.\"Confirmation\")" +
                                 " where \"Id\" = Alarmes_id;" +
                                 " RETURN new;" +
                                 " END;" +
                                 " $$; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
