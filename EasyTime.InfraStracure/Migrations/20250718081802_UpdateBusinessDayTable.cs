using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBusinessDayTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Users_BusinessOwnerId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_BusinessOwnerId",
                table: "Businesses");

            migrationBuilder.RenameColumn(
                name: "BusinessOwnerId",
                table: "Businesses",
                newName: "UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "BusinesOwnerId",
                table: "Businesses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinesOwnerId",
                table: "Businesses",
                column: "BusinesOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Users_BusinesOwnerId",
                table: "Businesses",
                column: "BusinesOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
