using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlanInformationPlanId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwnerPlans_PlanTime_PlanTimeId",
                table: "BusinessOwnerPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_PlansInformation_Plans_PlanId",
                table: "PlansInformation");

            migrationBuilder.DropIndex(
                name: "IX_PlansInformation_PlanId",
                table: "PlansInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanTime",
                table: "PlanTime");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "PlansInformation");

            migrationBuilder.RenameTable(
                name: "PlanTime",
                newName: "PlanTimes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanTimes",
                table: "PlanTimes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlansInformation_PlandId",
                table: "PlansInformation",
                column: "PlandId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwnerPlans_PlanTimes_PlanTimeId",
                table: "BusinessOwnerPlans",
                column: "PlanTimeId",
                principalTable: "PlanTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlansInformation_Plans_PlandId",
                table: "PlansInformation",
                column: "PlandId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessOwnerPlans_PlanTimes_PlanTimeId",
                table: "BusinessOwnerPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_PlansInformation_Plans_PlandId",
                table: "PlansInformation");

            migrationBuilder.DropIndex(
                name: "IX_PlansInformation_PlandId",
                table: "PlansInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanTimes",
                table: "PlanTimes");

            migrationBuilder.RenameTable(
                name: "PlanTimes",
                newName: "PlanTime");

            migrationBuilder.AddColumn<long>(
                name: "PlanId",
                table: "PlansInformation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanTime",
                table: "PlanTime",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PlansInformation_PlanId",
                table: "PlansInformation",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessOwnerPlans_PlanTime_PlanTimeId",
                table: "BusinessOwnerPlans",
                column: "PlanTimeId",
                principalTable: "PlanTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlansInformation_Plans_PlanId",
                table: "PlansInformation",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
