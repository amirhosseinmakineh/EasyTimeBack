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
                    BusinesOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBusinesOwner = table.Column<bool>(type: "bit", nullable: false),
                    TokenForChangePassword = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpireChangePasswordToken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinesRegiones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinesOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "BusinesNeighberhoodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinesOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "BusinesOwners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    NeighberhoodId = table.Column<long>(type: "bigint", nullable: false),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinesCitiyId = table.Column<long>(type: "bigint", nullable: false),
                    BusinesRegionId = table.Column<long>(type: "bigint", nullable: false),
                    BusinesNeighberhoodId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinesOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinesOwners_BusinesCityes_BusinesCitiyId",
                        column: x => x.BusinesCitiyId,
                        principalTable: "BusinesCityes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BusinesOwners_BusinesNeighberhoodes_BusinesNeighberhoodId",
                        column: x => x.BusinesNeighberhoodId,
                        principalTable: "BusinesNeighberhoodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BusinesOwners_BusinesRegiones_BusinesRegionId",
                        column: x => x.BusinesRegionId,
                        principalTable: "BusinesRegiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BusinesOwners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinesOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    NeighberhoodId = table.Column<long>(type: "bigint", nullable: false),
                    BusinessOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinesCityId = table.Column<long>(type: "bigint", nullable: false),
                    BusinesRegionId = table.Column<long>(type: "bigint", nullable: false),
                    BusinesNeighberhoodId = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateObjectDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateEntityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinesCityes_BusinesCityId",
                        column: x => x.BusinesCityId,
                        principalTable: "BusinesCityes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinesNeighberhoodes_BusinesNeighberhoodId",
                        column: x => x.BusinesNeighberhoodId,
                        principalTable: "BusinesNeighberhoodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinesOwners_BusinessOwnerId",
                        column: x => x.BusinessOwnerId,
                        principalTable: "BusinesOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinesRegiones_BusinesRegionId",
                        column: x => x.BusinesRegionId,
                        principalTable: "BusinesRegiones",
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
                name: "IX_BusinesOwners_BusinesCitiyId",
                table: "BusinesOwners",
                column: "BusinesCitiyId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesOwners_BusinesNeighberhoodId",
                table: "BusinesOwners",
                column: "BusinesNeighberhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesOwners_BusinesRegionId",
                table: "BusinesOwners",
                column: "BusinesRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinesOwners_UserId",
                table: "BusinesOwners",
                column: "UserId");

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
                name: "IX_Businesses_BusinesCityId",
                table: "Businesses",
                column: "BusinesCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinesNeighberhoodId",
                table: "Businesses",
                column: "BusinesNeighberhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinesRegionId",
                table: "Businesses",
                column: "BusinesRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessOwnerId",
                table: "Businesses",
                column: "BusinessOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinesOwnerTimes");

            migrationBuilder.DropTable(
                name: "BusinesOwnerDays");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "BusinesOwners");

            migrationBuilder.DropTable(
                name: "BusinesNeighberhoodes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BusinesRegiones");

            migrationBuilder.DropTable(
                name: "BusinesCityes");
        }
    }
}
