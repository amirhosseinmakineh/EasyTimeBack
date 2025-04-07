using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsCheckExpireDatatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpireChangePasswordToken",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireChangePasswordToken",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpireChangePasswordToken",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsExpireChangePasswordToken",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
