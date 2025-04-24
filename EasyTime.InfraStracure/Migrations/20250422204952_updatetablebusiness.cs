using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class updatetablebusiness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinesCityes_BusinesCityId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinesNeighberhoodes_BusinesNeighberhoodId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinesOwners_BusinessOwnerId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinesRegiones_BusinesRegionId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinesCityId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinesNeighberhoodId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinesRegionId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinessOwnerId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinesCityId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinesNeighberhoodId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinesRegionId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "BusinessOwnerId",
                table: "Businesses");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinesOwnerId",
                table: "Businesses",
                column: "BusinesOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_CityId",
                table: "Businesses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_NeighberhoodId",
                table: "Businesses",
                column: "NeighberhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_RegionId",
                table: "Businesses",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinesCityes_CityId",
                table: "Businesses",
                column: "CityId",
                principalTable: "BusinesCityes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinesNeighberhoodes_NeighberhoodId",
                table: "Businesses",
                column: "NeighberhoodId",
                principalTable: "BusinesNeighberhoodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinesOwners_BusinesOwnerId",
                table: "Businesses",
                column: "BusinesOwnerId",
                principalTable: "BusinesOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinesRegiones_RegionId",
                table: "Businesses",
                column: "RegionId",
                principalTable: "BusinesRegiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinesCityes_CityId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinesNeighberhoodes_NeighberhoodId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinesOwners_BusinesOwnerId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_BusinesRegiones_RegionId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinesOwnerId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_CityId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_NeighberhoodId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_RegionId",
                table: "Businesses");

            migrationBuilder.AddColumn<long>(
                name: "BusinesCityId",
                table: "Businesses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BusinesNeighberhoodId",
                table: "Businesses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BusinesRegionId",
                table: "Businesses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessOwnerId",
                table: "Businesses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinesCityId",
                table: "Businesses",
                column: "BusinesCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinesNeighberhoodId",
                table: "Businesses",
                column: "BusinesNeighberhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinesRegionId",
                table: "Businesses",
                column: "BusinesRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessOwnerId",
                table: "Businesses",
                column: "BusinessOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinesCityes_BusinesCityId",
                table: "Businesses",
                column: "BusinesCityId",
                principalTable: "BusinesCityes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinesNeighberhoodes_BusinesNeighberhoodId",
                table: "Businesses",
                column: "BusinesNeighberhoodId",
                principalTable: "BusinesNeighberhoodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinesOwners_BusinessOwnerId",
                table: "Businesses",
                column: "BusinessOwnerId",
                principalTable: "BusinesOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_BusinesRegiones_BusinesRegionId",
                table: "Businesses",
                column: "BusinesRegionId",
                principalTable: "BusinesRegiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
