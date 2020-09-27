using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class enemyLootSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}