using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnSessionUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_User_UserId",
                schema: "YOUR_SCHEMA",
                table: "Session");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "YOUR_SCHEMA",
                table: "Session",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Session_UserId",
                schema: "YOUR_SCHEMA",
                table: "Session",
                newName: "IX_Session_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "Session",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "Session");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "YOUR_SCHEMA",
                table: "Session",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Session_user_id",
                schema: "YOUR_SCHEMA",
                table: "Session",
                newName: "IX_Session_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_User_UserId",
                schema: "YOUR_SCHEMA",
                table: "Session",
                column: "UserId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
