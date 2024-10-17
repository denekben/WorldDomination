using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AchievmentsTest_UserWrite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                schema: "User",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 268, DateTimeKind.Utc).AddTicks(8789),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 314, DateTimeKind.Utc).AddTicks(7488));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                schema: "User",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 268, DateTimeKind.Utc).AddTicks(8503),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 314, DateTimeKind.Utc).AddTicks(7205));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AchievedTime",
                schema: "User",
                table: "UserAchievments",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 270, DateTimeKind.Utc).AddTicks(5172),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 316, DateTimeKind.Utc).AddTicks(6854));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                schema: "User",
                table: "Achievments",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 269, DateTimeKind.Utc).AddTicks(5053),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 315, DateTimeKind.Utc).AddTicks(4662));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                schema: "User",
                table: "Achievments",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 269, DateTimeKind.Utc).AddTicks(4799),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 315, DateTimeKind.Utc).AddTicks(4428));

            migrationBuilder.InsertData(
                schema: "User",
                table: "Achievments",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("14cfe78a-3794-4adf-b773-d70fc1491fdd"), "Выиграйте игру в роли президента", "Великий вождь" },
                    { new Guid("9394332a-f4fe-4121-9909-a3c2a5761253"), "Произведите 5 ядерный бомб", "Давай нападай" },
                    { new Guid("a20b2a5d-a2ac-45d7-a949-e148b3e37cda"), "Выиграйте, будучи обложенным санкциями всех стран", "Сильный и независимый" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "User",
                table: "Achievments",
                keyColumn: "Id",
                keyValue: new Guid("14cfe78a-3794-4adf-b773-d70fc1491fdd"));

            migrationBuilder.DeleteData(
                schema: "User",
                table: "Achievments",
                keyColumn: "Id",
                keyValue: new Guid("9394332a-f4fe-4121-9909-a3c2a5761253"));

            migrationBuilder.DeleteData(
                schema: "User",
                table: "Achievments",
                keyColumn: "Id",
                keyValue: new Guid("a20b2a5d-a2ac-45d7-a949-e148b3e37cda"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                schema: "User",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 314, DateTimeKind.Utc).AddTicks(7488),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 268, DateTimeKind.Utc).AddTicks(8789));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                schema: "User",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 314, DateTimeKind.Utc).AddTicks(7205),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 268, DateTimeKind.Utc).AddTicks(8503));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AchievedTime",
                schema: "User",
                table: "UserAchievments",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 316, DateTimeKind.Utc).AddTicks(6854),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 270, DateTimeKind.Utc).AddTicks(5172));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                schema: "User",
                table: "Achievments",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 315, DateTimeKind.Utc).AddTicks(4662),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 269, DateTimeKind.Utc).AddTicks(5053));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                schema: "User",
                table: "Achievments",
                type: "timestamp without time zone",
                nullable: true,
                defaultValue: new DateTime(2024, 10, 15, 13, 7, 2, 315, DateTimeKind.Utc).AddTicks(4428),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 10, 16, 17, 30, 58, 269, DateTimeKind.Utc).AddTicks(4799));
        }
    }
}
