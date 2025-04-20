using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinesCityes_BusinesOwners_BusinesOwnerId",
                table: "BusinesCityes");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinesNeighberhoodes_BusinesOwners_BusinesOwnerId",
                table: "BusinesNeighberhoodes");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinesRegiones_BusinesOwners_BusinesOwnerId",
                table: "BusinesRegiones");

            migrationBuilder.DropIndex(
                name: "IX_BusinesRegiones_BusinesOwnerId",
                table: "BusinesRegiones");

            migrationBuilder.DropIndex(
                name: "IX_BusinesNeighberhoodes_BusinesOwnerId",
                table: "BusinesNeighberhoodes");

            migrationBuilder.DropIndex(
                name: "IX_BusinesCityes_BusinesOwnerId",
                table: "BusinesCityes");

            migrationBuilder.AddColumn<long>(
                name: "BusinesCitiyId",
                table: "BusinesOwners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BusinesNeighberhoodId",
                table: "BusinesOwners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BusinesRegionId",
                table: "BusinesOwners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "BusinesOwners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "NeighberhoodId",
                table: "BusinesOwners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                table: "BusinesOwners",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_BusinesOwners_BusinesCitiyId",
                table: "BusinesOwners",
                column: "BusinesCitiyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesOwners_BusinesNeighberhoodId",
                table: "BusinesOwners",
                column: "BusinesNeighberhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesOwners_BusinesRegionId",
                table: "BusinesOwners",
                column: "BusinesRegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinesOwners_BusinesCityes_BusinesCitiyId",
                table: "BusinesOwners",
                column: "BusinesCitiyId",
                principalTable: "BusinesCityes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinesOwners_BusinesNeighberhoodes_BusinesNeighberhoodId",
                table: "BusinesOwners",
                column: "BusinesNeighberhoodId",
                principalTable: "BusinesNeighberhoodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinesOwners_BusinesRegiones_BusinesRegionId",
                table: "BusinesOwners",
                column: "BusinesRegionId",
                principalTable: "BusinesRegiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinesOwners_BusinesCityes_BusinesCitiyId",
                table: "BusinesOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinesOwners_BusinesNeighberhoodes_BusinesNeighberhoodId",
                table: "BusinesOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinesOwners_BusinesRegiones_BusinesRegionId",
                table: "BusinesOwners");

            migrationBuilder.DropIndex(
                name: "IX_BusinesOwners_BusinesCitiyId",
                table: "BusinesOwners");

            migrationBuilder.DropIndex(
                name: "IX_BusinesOwners_BusinesNeighberhoodId",
                table: "BusinesOwners");

            migrationBuilder.DropIndex(
                name: "IX_BusinesOwners_BusinesRegionId",
                table: "BusinesOwners");

            migrationBuilder.DropColumn(
                name: "BusinesCitiyId",
                table: "BusinesOwners");

            migrationBuilder.DropColumn(
                name: "BusinesNeighberhoodId",
                table: "BusinesOwners");

            migrationBuilder.DropColumn(
                name: "BusinesRegionId",
                table: "BusinesOwners");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "BusinesOwners");

            migrationBuilder.DropColumn(
                name: "NeighberhoodId",
                table: "BusinesOwners");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "BusinesOwners");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesRegiones_BusinesOwnerId",
                table: "BusinesRegiones",
                column: "BusinesOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesNeighberhoodes_BusinesOwnerId",
                table: "BusinesNeighberhoodes",
                column: "BusinesOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesCityes_BusinesOwnerId",
                table: "BusinesCityes",
                column: "BusinesOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinesCityes_BusinesOwners_BusinesOwnerId",
                table: "BusinesCityes",
                column: "BusinesOwnerId",
                principalTable: "BusinesOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinesNeighberhoodes_BusinesOwners_BusinesOwnerId",
                table: "BusinesNeighberhoodes",
                column: "BusinesOwnerId",
                principalTable: "BusinesOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinesRegiones_BusinesOwners_BusinesOwnerId",
                table: "BusinesRegiones",
                column: "BusinesOwnerId",
                principalTable: "BusinesOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
