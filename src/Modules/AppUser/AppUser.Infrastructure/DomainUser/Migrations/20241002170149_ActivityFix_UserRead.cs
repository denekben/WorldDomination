using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUser.Infrastructure.DomainUser.Migrations
{
    /// <inheritdoc />
    public partial class ActivityFix_UserRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "AppUser",
                table: "ActivityStatuses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "AppUser",
                table: "ActivityStatuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
