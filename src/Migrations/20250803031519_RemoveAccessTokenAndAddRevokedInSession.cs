using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAccessTokenAndAddRevokedInSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Session_access_token",
                schema: "YOUR_SCHEMA",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "access_token",
                schema: "YOUR_SCHEMA",
                table: "Session");

            migrationBuilder.AddColumn<bool>(
                name: "is_revoked",
                schema: "YOUR_SCHEMA",
                table: "Session",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "revoked_at",
                schema: "YOUR_SCHEMA",
                table: "Session",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_revoked",
                schema: "YOUR_SCHEMA",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "revoked_at",
                schema: "YOUR_SCHEMA",
                table: "Session");

            migrationBuilder.AddColumn<string>(
                name: "access_token",
                schema: "YOUR_SCHEMA",
                table: "Session",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Session_access_token",
                schema: "YOUR_SCHEMA",
                table: "Session",
                column: "access_token",
                unique: true);
        }
    }
}
