using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                schema: "YOUR_SCHEMA",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                schema: "YOUR_SCHEMA",
                table: "User",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_hash",
                schema: "YOUR_SCHEMA",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                schema: "YOUR_SCHEMA",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
