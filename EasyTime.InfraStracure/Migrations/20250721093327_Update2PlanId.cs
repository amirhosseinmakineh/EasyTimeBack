using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class Update2PlanId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlansInformation_Plans_PlandId",
                table: "PlansInformation");

            migrationBuilder.DropIndex(
                name: "IX_PlansInformation_PlandId",
                table: "PlansInformation");

            migrationBuilder.DropColumn(
                name: "PlandId",
                table: "PlansInformation");

            migrationBuilder.CreateIndex(
                name: "IX_PlansInformation_PlanId",
                table: "PlansInformation",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlansInformation_Plans_PlanId",
                table: "PlansInformation",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlansInformation_Plans_PlanId",
                table: "PlansInformation");

            migrationBuilder.DropIndex(
                name: "IX_PlansInformation_PlanId",
                table: "PlansInformation");

            migrationBuilder.AddColumn<long>(
                name: "PlandId",
                table: "PlansInformation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PlansInformation_PlandId",
                table: "PlansInformation",
                column: "PlandId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlansInformation_Plans_PlandId",
                table: "PlansInformation",
                column: "PlandId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
