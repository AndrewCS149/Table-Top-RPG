using Microsoft.EntityFrameworkCore.Migrations;

namespace Crypts_And_Coders.Migrations
{
    public partial class addedEnemiesInLocationDTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Location_LocationId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_LocationId",
                table: "Character");

            migrationBuilder.AddColumn<int>(
                name: "LocationDTOId",
                table: "EnemyInLocation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLocationId",
                table: "Character",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocationDTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Character_LocationDTO_CurrentLocationId",
                table: "Character",
                column: "CurrentLocationId",
                principalTable: "LocationDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyInLocation_LocationDTO_LocationDTOId",
                table: "EnemyInLocation",
                column: "LocationDTOId",
                principalTable: "LocationDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_LocationDTO_CurrentLocationId",
                table: "Character");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemyInLocation_LocationDTO_LocationDTOId",
                table: "EnemyInLocation");

            migrationBuilder.DropTable(
                name: "LocationDTO");

            migrationBuilder.DropIndex(
                name: "IX_EnemyInLocation_LocationDTOId",
                table: "EnemyInLocation");

            migrationBuilder.DropIndex(
                name: "IX_Character_CurrentLocationId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "LocationDTOId",
                table: "EnemyInLocation");

            migrationBuilder.DropColumn(
                name: "CurrentLocationId",
                table: "Character");

            migrationBuilder.CreateIndex(
                name: "IX_Character_LocationId",
                table: "Character",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Location_LocationId",
                table: "Character",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}