using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class LogTableUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseContent",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ResponseContentType",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ResponseStatusCode",
                table: "Logs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponseContent",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseContentType",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponseStatusCode",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}