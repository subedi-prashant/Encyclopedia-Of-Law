using Microsoft.EntityFrameworkCore.Migrations;

namespace Encyclopedia_Of_Laws.Migrations
{
    public partial class AddedColumnsForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CitizenshipNumber",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseNumber",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CitizenshipNumber",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Specialization",
                schema: "security",
                table: "Users");
        }
    }
}
