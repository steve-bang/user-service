using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class TestRenameTbaleNameUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_users_UserId",
                schema: "YOUR_SCHEMA",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                schema: "YOUR_SCHEMA",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermission",
                schema: "YOUR_SCHEMA",
                table: "RolePermission");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "YOUR_SCHEMA",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "YOUR_SCHEMA",
                newName: "User",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "YOUR_SCHEMA",
                newName: "User_Role",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "RolePermission",
                schema: "YOUR_SCHEMA",
                newName: "Role_Permission",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "IX_User_Role_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_RoleId_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "IX_Role_Permission_RoleId_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "IX_Role_Permission_PermissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Role",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role_Permission",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                column: "Id");

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
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Role",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role_Permission",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission");

            migrationBuilder.RenameTable(
                name: "User_Role",
                schema: "YOUR_SCHEMA",
                newName: "UserRoles",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "YOUR_SCHEMA",
                newName: "users",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "Role_Permission",
                schema: "YOUR_SCHEMA",
                newName: "RolePermission",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameIndex(
                name: "IX_User_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permission_RoleId_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "RolePermission",
                newName: "IX_RolePermission_RoleId_PermissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permission_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "RolePermission",
                newName: "IX_RolePermission_PermissionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "YOUR_SCHEMA",
                table: "Role",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                schema: "YOUR_SCHEMA",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermission",
                schema: "YOUR_SCHEMA",
                table: "RolePermission",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                schema: "YOUR_SCHEMA",
                table: "RolePermission",
                column: "PermissionId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "RolePermission",
                column: "RoleId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Role_RoleId",
                schema: "YOUR_SCHEMA",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_users_UserId",
                schema: "YOUR_SCHEMA",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
