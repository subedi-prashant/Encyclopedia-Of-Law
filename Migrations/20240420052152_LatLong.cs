using Microsoft.EntityFrameworkCore.Migrations;

namespace Encyclopedia_Of_Laws.Migrations
{
    public partial class LatLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "LawyerInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Long",
                table: "LawyerInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "LawyerInfos");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "LawyerInfos");
        }
    }
}
