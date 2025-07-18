using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBusinessTimeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessTimes_BusinesOwnerTimes_BusinessOwnerTimeId",
                table: "BusinessTimes");

            migrationBuilder.DropIndex(
                name: "IX_BusinessTimes_BusinessOwnerTimeId",
                table: "BusinessTimes");

            migrationBuilder.DropColumn(
                name: "BusinessOwnerTimeId",
                table: "BusinessTimes");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTimes_TimeId",
                table: "BusinessTimes",
                column: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessTimes_BusinesOwnerTimes_TimeId",
                table: "BusinessTimes",
                column: "TimeId",
                principalTable: "BusinesOwnerTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessTimes_BusinesOwnerTimes_TimeId",
                table: "BusinessTimes");

            migrationBuilder.DropIndex(
                name: "IX_BusinessTimes_TimeId",
                table: "BusinessTimes");

            migrationBuilder.AddColumn<long>(
                name: "BusinessOwnerTimeId",
                table: "BusinessTimes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTimes_BusinessOwnerTimeId",
                table: "BusinessTimes",
                column: "BusinessOwnerTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessTimes_BusinesOwnerTimes_BusinessOwnerTimeId",
                table: "BusinessTimes",
                column: "BusinessOwnerTimeId",
                principalTable: "BusinesOwnerTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
