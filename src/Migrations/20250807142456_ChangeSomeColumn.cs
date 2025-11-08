using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSomeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Permission_Permission_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Permission_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_User_UserId",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_User_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "IX_User_Role_role_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "permission_id");

            migrationBuilder.RenameColumn(
                name: "AssignedAt",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "assigned_at");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permission_RoleId_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "IX_Role_Permission_role_id_permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permission_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "IX_Role_Permission_permission_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Permission_Permission_permission_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                column: "permission_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Permission_Role_role_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                column: "role_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_Role_role_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                column: "role_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
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
                name: "FK_Role_Permission_Permission_permission_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Permission_Role_role_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_Role_role_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.RenameColumn(
                name: "role_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Role_role_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "IX_User_Role_RoleId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "permission_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "PermissionId");

            migrationBuilder.RenameColumn(
                name: "assigned_at",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "AssignedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permission_role_id_permission_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "IX_Role_Permission_RoleId_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permission_permission_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "IX_Role_Permission_PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Permission_Permission_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                column: "PermissionId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Permission_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                column: "RoleId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                column: "RoleId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
