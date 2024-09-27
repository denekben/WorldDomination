using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUser.Infrastructure.DomainUser.Migrations
{
    /// <inheritdoc />
    public partial class Init_UserRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AppUser");

            migrationBuilder.CreateTable(
                name: "Achievments",
                schema: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityStatuses",
                schema: "AppUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsInGameStatus = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    RoundNumber = table.Column<string>(type: "text", nullable: false),
                    GameRole = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStatuses", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "AppUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ActivityStatusUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_ActivityStatuses_ActivityStatusUserId",
                        column: x => x.ActivityStatusUserId,
                        principalSchema: "AppUser",
                        principalTable: "ActivityStatuses",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievments",
                schema: "AppUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AchievmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    AchievedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievments", x => new { x.UserId, x.AchievmentId });
                    table.ForeignKey(
                        name: "FK_UserAchievments_Achievments_AchievmentId",
                        column: x => x.AchievmentId,
                        principalSchema: "AppUser",
                        principalTable: "Achievments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAchievments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "AppUser",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievments_AchievmentId",
                schema: "AppUser",
                table: "UserAchievments",
                column: "AchievmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ActivityStatusUserId",
                schema: "AppUser",
                table: "Users",
                column: "ActivityStatusUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAchievments",
                schema: "AppUser");

            migrationBuilder.DropTable(
                name: "Achievments",
                schema: "AppUser");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "AppUser");

            migrationBuilder.DropTable(
                name: "ActivityStatuses",
                schema: "AppUser");
        }
    }
}
