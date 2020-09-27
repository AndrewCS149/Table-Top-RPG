using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Crypts_And_Coders.Migrations
{
    public partial class CreateLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestContentType = table.Column<string>(nullable: true),
                    RequestContent = table.Column<string>(nullable: true),
                    RequestUri = table.Column<string>(nullable: true),
                    RequestMethod = table.Column<string>(nullable: true),
                    RequestTimestamp = table.Column<DateTime>(nullable: true),
                    ResponseContentType = table.Column<string>(nullable: true),
                    ResponseContent = table.Column<string>(nullable: true),
                    ResponseStatusCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}