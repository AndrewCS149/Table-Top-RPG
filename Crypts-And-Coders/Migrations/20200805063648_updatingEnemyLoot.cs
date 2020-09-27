using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class updatingEnemyLoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_LocationDTO_CurrentLocationId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Weapon_WeaponId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemyInLocation_LocationDTO_LocationDTOId",
                table: "EnemyInLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemyLoot_Character_CharacterId",
                table: "EnemyLoot");

            migrationBuilder.DropTable(
                name: "LocationDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnemyLoot",
                table: "EnemyLoot");

            migrationBuilder.DropIndex(
                name: "IX_EnemyInLocation_LocationDTOId",
                table: "EnemyInLocation");

            migrationBuilder.DropIndex(
                name: "IX_Character_CurrentLocationId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_WeaponId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "LocationDTOId",
                table: "EnemyInLocation");

            migrationBuilder.DropColumn(
                name: "CurrentLocationId",
                table: "Character");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "EnemyLoot",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EnemyId",
                table: "EnemyLoot",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnemyLoot",
                table: "EnemyLoot",
                columns: new[] { "EnemyId", "ItemId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyLoot_Enemy_EnemyId",
                table: "EnemyLoot",
                column: "EnemyId",
                principalTable: "Enemy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnemyLoot_Character_CharacterId",
                table: "EnemyLoot");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemyLoot_Enemy_EnemyId",
                table: "EnemyLoot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnemyLoot",
                table: "EnemyLoot");

            migrationBuilder.DropIndex(
                name: "IX_EnemyLoot_CharacterId",
                table: "EnemyLoot");

            migrationBuilder.DropColumn(
                name: "EnemyId",
                table: "EnemyLoot");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "EnemyLoot",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationDTOId",
                table: "EnemyInLocation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLocationId",
                table: "Character",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnemyLoot",
                table: "EnemyLoot",
                columns: new[] { "CharacterId", "ItemId" });

            migrationBuilder.CreateTable(
                name: "LocationDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationDTO", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnemyInLocation_LocationDTOId",
                table: "EnemyInLocation",
                column: "LocationDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_CurrentLocationId",
                table: "Character",
                column: "CurrentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_WeaponId",
                table: "Character",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_LocationDTO_CurrentLocationId",
                table: "Character",
                column: "CurrentLocationId",
                principalTable: "LocationDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Weapon_WeaponId",
                table: "Character",
                column: "WeaponId",
                principalTable: "Weapon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyInLocation_LocationDTO_LocationDTOId",
                table: "EnemyInLocation",
                column: "LocationDTOId",
                principalTable: "LocationDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyLoot_Character_CharacterId",
                table: "EnemyLoot",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}