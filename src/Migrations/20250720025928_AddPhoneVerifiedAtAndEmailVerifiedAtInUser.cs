using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneVerifiedAtAndEmailVerifiedAtInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "email_verified_at",
                schema: "YOUR_SCHEMA",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "phone_verified_at",
                schema: "YOUR_SCHEMA",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_verified_at",
                schema: "YOUR_SCHEMA",
                table: "User");

            migrationBuilder.DropColumn(
                name: "phone_verified_at",
                schema: "YOUR_SCHEMA",
                table: "User");
        }
    }
}
