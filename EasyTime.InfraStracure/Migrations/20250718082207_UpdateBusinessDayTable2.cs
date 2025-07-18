using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBusinessDayTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessDays_BusinesOwnerDays_BusinessOwnerDayId",
                table: "BusinessDays");

            migrationBuilder.DropIndex(
                name: "IX_BusinessDays_BusinessOwnerDayId",
                table: "BusinessDays");

            migrationBuilder.DropColumn(
                name: "BusinessOwnerDayId",
                table: "BusinessDays");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessDays_DayId",
                table: "BusinessDays",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessDays_BusinesOwnerDays_DayId",
                table: "BusinessDays",
                column: "DayId",
                principalTable: "BusinesOwnerDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessDays_BusinesOwnerDays_DayId",
                table: "BusinessDays");

            migrationBuilder.DropIndex(
                name: "IX_BusinessDays_DayId",
                table: "BusinessDays");

            migrationBuilder.AddColumn<long>(
                name: "BusinessOwnerDayId",
                table: "BusinessDays",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessDays_BusinessOwnerDayId",
                table: "BusinessDays",
                column: "BusinessOwnerDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessDays_BusinesOwnerDays_BusinessOwnerDayId",
                table: "BusinessDays",
                column: "BusinessOwnerDayId",
                principalTable: "BusinesOwnerDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
