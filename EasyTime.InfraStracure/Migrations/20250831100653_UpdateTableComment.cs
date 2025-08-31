using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BusinessId",
                table: "Comments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BusinessId",
                table: "Comments",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Businesses_BusinessId",
                table: "Comments",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Businesses_BusinessId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_BusinessId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Comments");
        }
    }
}
