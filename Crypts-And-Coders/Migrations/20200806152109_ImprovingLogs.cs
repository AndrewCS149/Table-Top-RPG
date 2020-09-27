using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Crypts_And_Coders.Migrations
{
    public partial class ImprovingLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestContent",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RequestContentType",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RequestMethod",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RequestTimestamp",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RequestUri",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Logs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "RequestContent",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestContentType",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestMethod",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestTimestamp",
                table: "Logs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestUri",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}