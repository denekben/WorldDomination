using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppUser.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeeded_UserRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AppUser");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "AppUser");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "AppUser");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "AppUser");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "AppUser");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "AppUser");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "AppUser");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "AppUser");

            migrationBuilder.InsertData(
                schema: "AppUser",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4fcfcc6e-f3ba-44a3-8ce3-e2ee38cb208f", null, "Member", "MEMBER" },
                    { "81d48fc7-040c-4b02-a8c9-03d1b9aa444c", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "AppUser",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fcfcc6e-f3ba-44a3-8ce3-e2ee38cb208f");

            migrationBuilder.DeleteData(
                schema: "AppUser",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81d48fc7-040c-4b02-a8c9-03d1b9aa444c");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "AppUser",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "AppUser",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "AppUser",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "AppUser",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "AppUser",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "AppUser",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "AppUser",
                newName: "AspNetRoleClaims");
        }
    }
}
