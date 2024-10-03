using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Encyclopedia_Of_Laws.Migrations
{
    public partial class AddRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lawyer_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Request_Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "(N'Pending')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_Requests_Lawyers",
                        column: x => x.Lawyer_ID,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_users",
                        column: x => x.User_ID,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Lawyer_ID",
                table: "Requests",
                column: "Lawyer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_User_ID",
                table: "Requests",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
