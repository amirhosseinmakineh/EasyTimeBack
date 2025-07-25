using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlanAndPlanTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Plans",
                newName: "BasePrice");

            migrationBuilder.AddColumn<float>(
                name: "AmountAdded",
                table: "PlanTimes",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountAdded",
                table: "PlanTimes");

            migrationBuilder.RenameColumn(
                name: "BasePrice",
                table: "Plans",
                newName: "Price");
        }
    }
}
