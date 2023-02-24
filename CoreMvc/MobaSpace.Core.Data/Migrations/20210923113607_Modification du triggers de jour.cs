using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Modificationdutriggersdejour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE OR REPLACE FUNCTION Jour_encrypt_insert() RETURNS trigger" +
                     " language plpgsql AS" +
                     " $$" +
                     " DECLARE" +
                     " new_nonce bytea = crypto_secretbox_noncegen();" +
                     " Jours_id bigint;" +
                     " BEGIN" +
                     " IF new.\"Id\" IS null then" +
                     " insert into mobaspace_data.\"Jours\"(nonce, \"PatientId\" , \"DateJour\") values(new_nonce, new.\"PatientId\", new.\"DateJour\") returning \"Id\" into Jours_id;" +
                     " ELSE" +
                     " insert into mobaspace_data.\"Jours\"(\"Id\", nonce, \"PatientId\", \"DateJour\") values (new.\"Id\", new_nonce, new.\"PatientId\", new.\"DateJour\") returning \"Id\" into Jours_id;" +
                     " END IF;" +
                     " update mobaspace_data.\"Jours\"" +
                     " set \"NbPas\" = crypto_secretbox(" +
                     " convert_to(Cast(new.\"NbPas\" As text), 'utf8')," +
                     " new_nonce," +
                     " key_id)," +
                     " \"TempsAllongeTotal\" = crypto_secretbox(" +
                     " convert_to(Cast(new.\"TempsAllongeTotal\" As text), 'utf8')," +
                     " new_nonce," +
                     " key_id)," +
                     " \"JourTraite\" = CASE WHEN(new.\"JourTraite\" IS NULL) THEN false ELSE new.\"JourTraite\" END" +
                     " where \"Id\" = Jours_id;" +
                     " RETURN new;" +
                     " END;" +
                     " $$; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
