using Microsoft.EntityFrameworkCore.Migrations;

namespace MobaSpace.Core.Data.Migrations
{
    public partial class Defaultvaluekey_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "key_id",
                schema: "mobaspace_data",
                table: "TrackersC",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "key_id",
                schema: "mobaspace_data",
                table: "TrackersC",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldDefaultValue: 0L);
        }
    }
}
