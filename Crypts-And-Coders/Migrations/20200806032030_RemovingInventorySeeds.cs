using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class RemovingInventorySeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Stat",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Charisma");

            migrationBuilder.UpdateData(
                table: "Stat",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Constitution");

            migrationBuilder.UpdateData(
                table: "Stat",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Dexterity");

            migrationBuilder.InsertData(
                table: "Stat",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Intelligence" },
                    { 5, "Strength" },
                    { 6, "Wisdom" }
                });

            migrationBuilder.UpdateData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 1, 1 },
                column: "Level",
                value: 5);

            migrationBuilder.UpdateData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 2, 2 },
                column: "Level",
                value: 2);

            migrationBuilder.UpdateData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 3, 2 },
                column: "Level",
                value: 10);

            migrationBuilder.InsertData(
                table: "StatSheet",
                columns: new[] { "StatId", "CharacterId", "Level" },
                values: new object[] { 4, 3, 4 });

            migrationBuilder.InsertData(
                table: "StatSheet",
                columns: new[] { "StatId", "CharacterId", "Level" },
                values: new object[] { 5, 3, 7 });

            migrationBuilder.InsertData(
                table: "StatSheet",
                columns: new[] { "StatId", "CharacterId", "Level" },
                values: new object[] { 6, 1, 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "Stat",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Stat",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stat",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "EnemyLoot",
                columns: new[] { "EnemyId", "ItemId", "CharacterId" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 1, 2, null },
                    { 2, 2, null },
                    { 2, 3, null },
                    { 3, 1, null },
                    { 3, 3, null }
                });

            migrationBuilder.UpdateData(
                table: "Stat",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Strength");

            migrationBuilder.UpdateData(
                table: "Stat",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Cunning");

            migrationBuilder.UpdateData(
                table: "Stat",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Constitution");

            migrationBuilder.UpdateData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 1, 1 },
                column: "Level",
                value: 0);

            migrationBuilder.UpdateData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 2, 2 },
                column: "Level",
                value: 0);

            migrationBuilder.UpdateData(
                table: "StatSheet",
                keyColumns: new[] { "StatId", "CharacterId" },
                keyValues: new object[] { 3, 2 },
                column: "Level",
                value: 0);

            migrationBuilder.InsertData(
                table: "StatSheet",
                columns: new[] { "StatId", "CharacterId", "Level" },
                values: new object[,]
                {
                    { 2, 1, 0 },
                    { 1, 3, 0 },
                    { 3, 3, 0 }
                });
        }
    }
}