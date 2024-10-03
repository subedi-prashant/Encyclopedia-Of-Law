using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Encyclopedia_Of_Laws.Migrations
{
    public partial class ExtendIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                schema: "security",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "security",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "security",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                schema: "security",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                schema: "security",
                table: "Users");
        }
    }
}
