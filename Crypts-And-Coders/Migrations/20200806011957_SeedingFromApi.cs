using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class SeedingFromApi : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "BaseDamage",
                table: "Weapon",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Item",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BaseDamage",
                table: "Weapon",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "Item",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "Health Potion", 25 },
                    { 2, "Cup", 5 },
                    { 3, "Dungeon Key", 100 }
                });

            migrationBuilder.InsertData(
                table: "Weapon",
                columns: new[] { "Id", "BaseDamage", "Name", "Type" },
                values: new object[,]
                {
                    { 1, 15, "Claymore", "Close Range" },
                    { 2, 18, "Wizard Staff", "Magical" },
                    { 3, 10, "Longbow", "Long Range" }
                });
        }
    }
}