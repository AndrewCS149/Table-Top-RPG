using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnemyLoot_Character_CharacterId",
                table: "EnemyLoot");

            migrationBuilder.DropIndex(
                name: "IX_EnemyLoot_CharacterId",
                table: "EnemyLoot");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "EnemyLoot");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "EnemyLoot",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnemyLoot_CharacterId",
                table: "EnemyLoot",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyLoot_Character_CharacterId",
                table: "EnemyLoot",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
