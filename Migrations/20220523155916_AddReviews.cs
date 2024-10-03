using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Encyclopedia_Of_Laws.Migrations
{
    public partial class AddReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Requests",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_Request_ID",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "Request_ID",
                table: "Reviews",
                newName: "Request_Id");

            migrationBuilder.RenameColumn(
                name: "ReviewID",
                table: "Reviews",
                newName: "ReviewId");

            migrationBuilder.RenameColumn(
                name: "Review_Date",
                table: "Reviews",
                newName: "ReviewDate");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReviewDate",
                table: "Reviews",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewDate",
                table: "Requests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewStatus",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RequestId",
                table: "Reviews",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Requests_RequestId",
                table: "Reviews",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Requests_RequestId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RequestId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ReviewStatus",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Request_Id",
                table: "Reviews",
                newName: "Request_ID");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "Reviews",
                newName: "ReviewID");

            migrationBuilder.RenameColumn(
                name: "ReviewDate",
                table: "Reviews",
                newName: "Review_Date");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Review_Date",
                table: "Reviews",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Request_ID",
                table: "Reviews",
                column: "Request_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Requests",
                table: "Reviews",
                column: "Request_ID",
                principalTable: "Requests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
