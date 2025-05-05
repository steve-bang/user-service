using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConfigUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_User_UserId",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_User_UserId",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                column: "UserId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_User_UserId",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_User_UserId",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                column: "UserId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
