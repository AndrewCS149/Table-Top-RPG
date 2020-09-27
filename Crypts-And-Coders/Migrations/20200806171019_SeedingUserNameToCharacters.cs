using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class SeedingUserNameToCharacters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Class", "Species", "UserName" },
                values: new object[] { 9, 2, "Seed" });

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Class", "Species", "UserName" },
                values: new object[] { 7, 1, "Seed" });

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Class", "Species", "UserName" },
                values: new object[] { 2, 7, "Seed" });

            migrationBuilder.UpdateData(
                table: "Enemy",
                keyColumn: "Id",
                keyValue: 1,
                column: "Species",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Enemy",
                keyColumn: "Id",
                keyValue: 2,
                column: "Species",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Enemy",
                keyColumn: "Id",
                keyValue: 3,
                column: "Species",
                value: 11);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Class", "Species", "UserName" },
                values: new object[] { 3, 1, null });

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Class", "Species", "UserName" },
                values: new object[] { 0, 3, null });

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Class", "Species", "UserName" },
                values: new object[] { 4, 0, null });

            migrationBuilder.UpdateData(
                table: "Enemy",
                keyColumn: "Id",
                keyValue: 1,
                column: "Species",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Enemy",
                keyColumn: "Id",
                keyValue: 2,
                column: "Species",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Enemy",
                keyColumn: "Id",
                keyValue: 3,
                column: "Species",
                value: 8);
        }
    }
}