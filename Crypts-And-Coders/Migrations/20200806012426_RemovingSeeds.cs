using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class RemovingSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterInventory",
                keyColumns: new[] { "CharacterId", "ItemId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterInventory",
                keyColumns: new[] { "CharacterId", "ItemId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterInventory",
                keyColumns: new[] { "CharacterId", "ItemId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CharacterInventory",
                keyColumns: new[] { "CharacterId", "ItemId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "CharacterInventory",
                keyColumns: new[] { "CharacterId", "ItemId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "CharacterInventory",
                keyColumns: new[] { "CharacterId", "ItemId" },
                keyValues: new object[] { 3, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CharacterInventory",
                columns: new[] { "CharacterId", "ItemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 1 },
                    { 3, 3 }
                });
        }
    }
}