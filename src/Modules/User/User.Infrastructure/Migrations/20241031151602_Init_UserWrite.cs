using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_UserWrite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.CreateTable(
                name: "Achievments",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValue: new DateTime(2024, 10, 31, 15, 16, 2, 201, DateTimeKind.Utc).AddTicks(577)),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValue: new DateTime(2024, 10, 31, 15, 16, 2, 201, DateTimeKind.Utc).AddTicks(876))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ProfileImagePath = table.Column<string>(type: "text", nullable: false),
                    DefaultProfileImagePath = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValue: new DateTime(2024, 10, 31, 15, 16, 2, 200, DateTimeKind.Utc).AddTicks(4300)),
                    UpdatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValue: new DateTime(2024, 10, 31, 15, 16, 2, 200, DateTimeKind.Utc).AddTicks(4619))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievments",
                schema: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AchievmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    AchievedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValue: new DateTime(2024, 10, 31, 15, 16, 2, 202, DateTimeKind.Utc).AddTicks(845))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievments", x => new { x.UserId, x.AchievmentId });
                    table.ForeignKey(
                        name: "FK_UserAchievments_Achievments_AchievmentId",
                        column: x => x.AchievmentId,
                        principalSchema: "User",
                        principalTable: "Achievments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAchievments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStatuses",
                schema: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityStatus = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    RoundNumber = table.Column<int>(type: "integer", nullable: true),
                    GameRole = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatuses", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "User",
                table: "Achievments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2668f0fd-47e8-4dd2-88c9-8f2764226ca1"), "Выиграйте игру, будучи обложенным санкциями всех стран", "Сильный и независимый" },
                    { new Guid("7da60ff1-2a64-42c9-a180-b635a0736e32"), "Произведите 5 ядерных бомб", "Давай давай нападай" },
                    { new Guid("960d7087-1eff-4874-9cab-9c74a191dd7c"), "Сбросьте ядерную бомбу", "Радиоактивный пепел" },
                    { new Guid("caed487f-b136-4705-a5d3-2ea2d65d62ef"), "Выиграйте игру в роли президента", "Великий вождь" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievments_AchievmentId",
                schema: "User",
                table: "UserAchievments",
                column: "AchievmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAchievments",
                schema: "User");

            migrationBuilder.DropTable(
                name: "UserStatuses",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Achievments",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "User");
        }
    }
}
