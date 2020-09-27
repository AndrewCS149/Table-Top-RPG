using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Plagued by the great war, Murkden remains uninhabited from all intelligent life forms, although various beasts still dwell in the deep marshes.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Location",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Plagued by the great war, Murkden remains uninhibited from all intelligent life forms, although various beasts still dwell in the deep marshes.");
        }
    }
}