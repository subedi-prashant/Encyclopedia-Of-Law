using Microsoft.EntityFrameworkCore.Migrations;

namespace Encyclopedia_Of_Laws.Migrations
{
    public partial class SocialMedias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "LawyerInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "LawyerInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "LawyerInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "LawyerInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "LawyerInfos");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "LawyerInfos");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "LawyerInfos");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "LawyerInfos");
        }
    }
}
