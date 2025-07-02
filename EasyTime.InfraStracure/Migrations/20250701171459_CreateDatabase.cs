using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTime.InfraStracure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinesCityes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinesCityes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinesRegiones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinesCityId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinesRegiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinesRegiones_BusinesCityes_BusinesCityId",
                        column: x => x.BusinesCityId,
                        principalTable: "BusinesCityes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBusinesOwner = table.Column<bool>(type: "bit", nullable: false),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenForChangePassword = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpireChangePasswordToken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BusinesNeighberhoodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinesRegionId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinesNeighberhoodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinesNeighberhoodes_BusinesRegiones_BusinesRegionId",
                        column: x => x.BusinesRegionId,
                        principalTable: "BusinesRegiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    NeighberhoodId = table.Column<long>(type: "bigint", nullable: false),
                    BusinesOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinesCityes_CityId",
                        column: x => x.CityId,
                        principalTable: "BusinesCityes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinesNeighberhoodes_NeighberhoodId",
                        column: x => x.NeighberhoodId,
                        principalTable: "BusinesNeighberhoodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinesRegiones_RegionId",
                        column: x => x.RegionId,
                        principalTable: "BusinesRegiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Businesses_Users_BusinesOwnerId",
                        column: x => x.BusinesOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BusinesOwnerDays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    BusinessId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinesOwnerDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinesOwnerDays_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BusinesOwnerTimes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessOwnerDayId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessId = table.Column<long>(type: "bigint", nullable: false),
                    From = table.Column<TimeSpan>(type: "time", nullable: false),
                    To = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinesOwnerTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinesOwnerTimes_BusinesOwnerDays_BusinessOwnerDayId",
                        column: x => x.BusinessOwnerDayId,
                        principalTable: "BusinesOwnerDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BusinesOwnerTimes_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Reserves_BusinesOwnerTimes_BusinessOwnerTimeId",
                        column: x => x.BusinessOwnerTimeId,
                        principalTable: "BusinesOwnerTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinesNeighberhoodes_BusinesRegionId",
                table: "BusinesNeighberhoodes",
                column: "BusinesRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesOwnerDays_BusinessId",
                table: "BusinesOwnerDays",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesOwnerTimes_BusinessId",
                table: "BusinesOwnerTimes",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesOwnerTimes_BusinessOwnerDayId",
                table: "BusinesOwnerTimes",
                column: "BusinessOwnerDayId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesRegiones_BusinesCityId",
                table: "BusinesRegiones",
                column: "BusinesCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinesOwnerId",
                table: "Businesses",
                column: "BusinesOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_CityId",
                table: "Businesses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_NeighberhoodId",
                table: "Businesses",
                column: "NeighberhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_RegionId",
                table: "Businesses",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_BusinessOwnerDayId",
                table: "Reserves",
                column: "BusinessOwnerDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_BusinessOwnerTimeId",
                table: "Reserves",
                column: "BusinessOwnerTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserves");

            migrationBuilder.DropTable(
                name: "BusinesOwnerTimes");

            migrationBuilder.DropTable(
                name: "BusinesOwnerDays");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "BusinesNeighberhoodes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BusinesRegiones");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "BusinesCityes");
        }
    }
}
