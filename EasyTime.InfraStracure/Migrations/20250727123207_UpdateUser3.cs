﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileComplete",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsProfileComplete",
                table: "Users");
        }
    }
}
