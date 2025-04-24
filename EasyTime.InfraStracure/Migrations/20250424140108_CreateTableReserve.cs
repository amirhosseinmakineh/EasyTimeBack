using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableReserve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reserves",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessOwnerDayId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessOwnerTimeId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reserves_BusinesOwnerDays_BusinessOwnerDayId",
                        column: x => x.BusinessOwnerDayId,
                        principalTable: "BusinesOwnerDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserves_BusinesOwnerTimes_BusinessOwnerTimeId",
                        column: x => x.BusinessOwnerTimeId,
                        principalTable: "BusinesOwnerTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_BusinessOwnerDayId",
                table: "Reserves",
                column: "BusinessOwnerDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_BusinessOwnerTimeId",
                table: "Reserves",
                column: "BusinessOwnerTimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserves");
        }
    }
}
