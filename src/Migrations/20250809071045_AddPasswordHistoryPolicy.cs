using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHistoryPolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Password_History",
                schema: "YOUR_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    password_salt = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Password_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Password_History_User_user_id",
                        column: x => x.user_id,
                        principalSchema: "YOUR_SCHEMA",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Password_History_user_id",
                schema: "YOUR_SCHEMA",
                table: "Password_History",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Password_History",
                schema: "YOUR_SCHEMA");

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
    }
}
