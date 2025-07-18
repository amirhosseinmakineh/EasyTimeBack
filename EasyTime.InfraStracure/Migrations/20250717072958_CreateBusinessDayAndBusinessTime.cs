using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class CreateBusinessDayAndBusinessTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessDays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<long>(type: "bigint", nullable: false),
                    DayId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessOwnerDayId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessDays_BusinesOwnerDays_BusinessOwnerDayId",
                        column: x => x.BusinessOwnerDayId,
                        principalTable: "BusinesOwnerDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessDays_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTimes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessDayId = table.Column<long>(type: "bigint", nullable: false),
                    TimeId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessOwnerTimeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessTimes_BusinesOwnerTimes_BusinessOwnerTimeId",
                        column: x => x.BusinessOwnerTimeId,
                        principalTable: "BusinesOwnerTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BusinessTimes_BusinessDays_BusinessDayId",
                        column: x => x.BusinessDayId,
                        principalTable: "BusinessDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessDays_BusinessId",
                table: "BusinessDays",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessDays_BusinessOwnerDayId",
                table: "BusinessDays",
                column: "BusinessOwnerDayId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTimes_BusinessDayId",
                table: "BusinessTimes",
                column: "BusinessDayId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTimes_BusinessOwnerTimeId",
                table: "BusinessTimes",
                column: "BusinessOwnerTimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessTimes");

            migrationBuilder.DropTable(
                name: "BusinessDays");
        }
    }
}
