using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class ReaddingSomeSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LocationId", "WeaponId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LocationId", "WeaponId" },
                values: new object[] { 3, 3 });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "Health Potion", "25 gp" },
                    { 2, "Cup", "5 gp" },
                    { 3, "Dungeon Key", "100 gp" }
                });

            migrationBuilder.InsertData(
                table: "Weapon",
                columns: new[] { "Id", "BaseDamage", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "1d4", "Claymore", "Close Range" },
                    { 2, "1d8", "Wizard Staff", "Magical" },
                    { 3, "1d6", "Longbow", "Long Range" }
                });

            migrationBuilder.InsertData(
                table: "CharacterInventory",
                columns: new[] { "CharacterId", "ItemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "EnemyLoot",
                columns: new[] { "EnemyId", "ItemId", "CharacterId" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 3, 1, null },
                    { 1, 2, null },
                    { 2, 2, null },
                    { 2, 3, null },
                    { 3, 3, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "EnemyLoot",
                keyColumns: new[] { "EnemyId", "ItemId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "EnemyLoot",
                keyColumns: new[] { "EnemyId", "ItemId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "EnemyLoot",
                keyColumns: new[] { "EnemyId", "ItemId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "EnemyLoot",
                keyColumns: new[] { "EnemyId", "ItemId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "EnemyLoot",
                keyColumns: new[] { "EnemyId", "ItemId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "EnemyLoot",
                keyColumns: new[] { "EnemyId", "ItemId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Weapon",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Weapon",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Weapon",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LocationId", "WeaponId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LocationId", "WeaponId" },
                values: new object[] { 1, 1 });
        }
    }
}