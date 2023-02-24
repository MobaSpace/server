using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Miseajoursdesindexs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"Observables\"', 'Id')," +
                                " (SELECT MAX(\"Id\") FROM mobaspace_data.\"Observables\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"Trackers\"', 'Id')," +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"Trackers\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"Alarmes\"', 'Id'), " +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"Alarmes\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"Patients\"', 'Id')," +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"Patients\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"Nuits\"', 'Id')," +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"Nuits\"));");

            migrationBuilder.Sql("SELECT SETVAL(pg_get_serial_sequence('mobaspace_data.\"Jours\"', 'Id')," +
                                 " (SELECT MAX(\"Id\") FROM mobaspace_data.\"Jours\"));");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
