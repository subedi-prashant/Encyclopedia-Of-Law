using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Encyclopedia_Of_Laws.Migrations
{
    public partial class AddedClientLetter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientLetters",
                columns: table => new
                {
                    LetterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lawyer_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LetterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LetterStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientLetters", x => x.LetterID);
                    table.ForeignKey(
                        name: "FK_ClientLetter_Lawyers",
                        column: x => x.Lawyer_ID,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientLetter_users",
                        column: x => x.User_ID,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientLetters_Lawyer_ID",
                table: "ClientLetters",
                column: "Lawyer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientLetters_User_ID",
                table: "ClientLetters",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientLetters");
        }
    }
}
