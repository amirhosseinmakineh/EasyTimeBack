using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlanTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusinessOwnerPlanId",
                table: "PlanTimes",
                newName: "PlandId");

            migrationBuilder.AddColumn<long>(
                name: "PlanTimeId",
                table: "Plans",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_PlanTimeId",
                table: "Plans",
                column: "PlanTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_PlanTimes_PlanTimeId",
                table: "Plans",
                column: "PlanTimeId",
                principalTable: "PlanTimes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_PlanTimes_PlanTimeId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_PlanTimeId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "PlanTimeId",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "PlandId",
                table: "PlanTimes",
                newName: "BusinessOwnerPlanId");
        }
    }
}
