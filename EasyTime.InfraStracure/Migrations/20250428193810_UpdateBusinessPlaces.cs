using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBusinessPlaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "BusinesOwnerId",
                table: "BusinesRegiones");

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

            migrationBuilder.DropColumn(
                name: "BusinesOwnerId",
                table: "BusinesNeighberhoodes");

            migrationBuilder.DropColumn(
                name: "BusinesOwnerId",
                table: "BusinesCityes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BusinesOwnerId",
                table: "BusinesRegiones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddColumn<Guid>(
                name: "BusinesOwnerId",
                table: "BusinesNeighberhoodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinesOwnerId",
                table: "BusinesCityes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinesOwners_BusinesNeighberhoodes_BusinesNeighberhoodId",
                table: "BusinesOwners",
                column: "BusinesNeighberhoodId",
                principalTable: "BusinesNeighberhoodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinesOwners_BusinesRegiones_BusinesRegionId",
                table: "BusinesOwners",
                column: "BusinesRegionId",
                principalTable: "BusinesRegiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
