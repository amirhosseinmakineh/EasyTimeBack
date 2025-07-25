using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class CreatePlansTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireDate",
                table: "Plans");

            migrationBuilder.AddColumn<bool>(
                name: "IsExpire",
                table: "UserBusinessOwners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExpire",
                table: "BusinessOwnerPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "PlanTimeId",
                table: "BusinessOwnerPlans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PlanTime",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessOwnerPlanId = table.Column<long>(type: "bigint", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTime", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwnerPlans_PlanTimeId",
                table: "BusinessOwnerPlans",
                column: "PlanTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwnerPlans_PlanTime_PlanTimeId",
                table: "BusinessOwnerPlans",
                column: "PlanTimeId",
                principalTable: "PlanTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwnerPlans_PlanTime_PlanTimeId",
                table: "BusinessOwnerPlans");

            migrationBuilder.DropTable(
                name: "PlanTime");

            migrationBuilder.DropIndex(
                name: "IX_BusinessOwnerPlans_PlanTimeId",
                table: "BusinessOwnerPlans");

            migrationBuilder.DropColumn(
                name: "IsExpire",
                table: "UserBusinessOwners");

            migrationBuilder.DropColumn(
                name: "IsExpire",
                table: "BusinessOwnerPlans");

            migrationBuilder.DropColumn(
                name: "PlanTimeId",
                table: "BusinessOwnerPlans");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                table: "Plans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
