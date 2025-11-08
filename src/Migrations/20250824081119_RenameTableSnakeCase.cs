using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otp_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "Otp");

            migrationBuilder.DropForeignKey(
                name: "FK_Password_History_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "Password_History");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Permission_Permission_permission_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Permission_Role_role_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantSettingEntity_tenant_TenantId",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Identity_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "User_Identity");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_Role_role_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Role",
                schema: "YOUR_SCHEMA",
                table: "User_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Identity",
                schema: "YOUR_SCHEMA",
                table: "User_Identity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_System_Log",
                schema: "YOUR_SCHEMA",
                table: "System_Log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Session",
                schema: "YOUR_SCHEMA",
                table: "Session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role_Permission",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "YOUR_SCHEMA",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                schema: "YOUR_SCHEMA",
                table: "Permission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Password_History",
                schema: "YOUR_SCHEMA",
                table: "Password_History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Otp",
                schema: "YOUR_SCHEMA",
                table: "Otp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenantSettingEntity",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity");

            migrationBuilder.DropIndex(
                name: "IX_TenantSettingEntity_TenantId",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity");

            migrationBuilder.RenameTable(
                name: "User_Role",
                schema: "YOUR_SCHEMA",
                newName: "user_role",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "User_Identity",
                schema: "YOUR_SCHEMA",
                newName: "user_identity",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "YOUR_SCHEMA",
                newName: "user",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "System_Log",
                schema: "YOUR_SCHEMA",
                newName: "system_log",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "Session",
                schema: "YOUR_SCHEMA",
                newName: "session",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "Role_Permission",
                schema: "YOUR_SCHEMA",
                newName: "role_permission",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "YOUR_SCHEMA",
                newName: "role",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "Permission",
                schema: "YOUR_SCHEMA",
                newName: "permission",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "Password_History",
                schema: "YOUR_SCHEMA",
                newName: "password_history",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "Otp",
                schema: "YOUR_SCHEMA",
                newName: "otp",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "TenantSettingEntity",
                schema: "YOUR_SCHEMA",
                newName: "tenant_setting",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameIndex(
                name: "IX_User_Role_role_id",
                schema: "YOUR_SCHEMA",
                table: "user_role",
                newName: "IX_user_role_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_User_Identity_user_id",
                schema: "YOUR_SCHEMA",
                table: "user_identity",
                newName: "IX_user_identity_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Session_user_id",
                schema: "YOUR_SCHEMA",
                table: "session",
                newName: "IX_session_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Session_refresh_token",
                schema: "YOUR_SCHEMA",
                table: "session",
                newName: "IX_session_refresh_token");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permission_role_id_permission_id",
                schema: "YOUR_SCHEMA",
                table: "role_permission",
                newName: "IX_role_permission_role_id_permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Permission_permission_id",
                schema: "YOUR_SCHEMA",
                table: "role_permission",
                newName: "IX_role_permission_permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_Role_name",
                schema: "YOUR_SCHEMA",
                table: "role",
                newName: "IX_role_name");

            migrationBuilder.RenameIndex(
                name: "IX_Permission_code",
                schema: "YOUR_SCHEMA",
                table: "permission",
                newName: "IX_permission_code");

            migrationBuilder.RenameIndex(
                name: "IX_Password_History_user_id",
                schema: "YOUR_SCHEMA",
                table: "password_history",
                newName: "IX_password_history_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Otp_user_id",
                schema: "YOUR_SCHEMA",
                table: "otp",
                newName: "IX_otp_user_id");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                newName: "tenant_id");

            migrationBuilder.RenameColumn(
                name: "SupportUrl",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                newName: "support_url");

            migrationBuilder.RenameColumn(
                name: "SupportEmail",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                newName: "support_email");

            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                newName: "logo_url");

            migrationBuilder.RenameColumn(
                name: "FriendlyName",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                newName: "friendly_name");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId1",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_role",
                schema: "YOUR_SCHEMA",
                table: "user_role",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_identity",
                schema: "YOUR_SCHEMA",
                table: "user_identity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_system_log",
                schema: "YOUR_SCHEMA",
                table: "system_log",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_session",
                schema: "YOUR_SCHEMA",
                table: "session",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_permission",
                schema: "YOUR_SCHEMA",
                table: "role_permission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role",
                schema: "YOUR_SCHEMA",
                table: "role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_permission",
                schema: "YOUR_SCHEMA",
                table: "permission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_password_history",
                schema: "YOUR_SCHEMA",
                table: "password_history",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_otp",
                schema: "YOUR_SCHEMA",
                table: "otp",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tenant_setting",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_setting_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_setting_TenantId1",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                column: "TenantId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_otp_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "otp",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_password_history_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "password_history",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permission_permission_permission_id",
                schema: "YOUR_SCHEMA",
                table: "role_permission",
                column: "permission_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permission_role_role_id",
                schema: "YOUR_SCHEMA",
                table: "role_permission",
                column: "role_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_session_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "session",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tenant_setting_tenant_TenantId1",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                column: "TenantId1",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "tenant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tenant_setting_tenant_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                column: "tenant_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_identity_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "user_identity",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_role_role_id",
                schema: "YOUR_SCHEMA",
                table: "user_role",
                column: "role_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "user_role",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_otp_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "otp");

            migrationBuilder.DropForeignKey(
                name: "FK_password_history_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "password_history");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permission_permission_permission_id",
                schema: "YOUR_SCHEMA",
                table: "role_permission");

            migrationBuilder.DropForeignKey(
                name: "FK_role_permission_role_role_id",
                schema: "YOUR_SCHEMA",
                table: "role_permission");

            migrationBuilder.DropForeignKey(
                name: "FK_session_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "session");

            migrationBuilder.DropForeignKey(
                name: "FK_tenant_setting_tenant_TenantId1",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting");

            migrationBuilder.DropForeignKey(
                name: "FK_tenant_setting_tenant_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting");

            migrationBuilder.DropForeignKey(
                name: "FK_user_identity_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "user_identity");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_role_role_id",
                schema: "YOUR_SCHEMA",
                table: "user_role");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_user_user_id",
                schema: "YOUR_SCHEMA",
                table: "user_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_role",
                schema: "YOUR_SCHEMA",
                table: "user_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_identity",
                schema: "YOUR_SCHEMA",
                table: "user_identity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_system_log",
                schema: "YOUR_SCHEMA",
                table: "system_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_session",
                schema: "YOUR_SCHEMA",
                table: "session");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_permission",
                schema: "YOUR_SCHEMA",
                table: "role_permission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role",
                schema: "YOUR_SCHEMA",
                table: "role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permission",
                schema: "YOUR_SCHEMA",
                table: "permission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_password_history",
                schema: "YOUR_SCHEMA",
                table: "password_history");

            migrationBuilder.DropPrimaryKey(
                name: "PK_otp",
                schema: "YOUR_SCHEMA",
                table: "otp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tenant_setting",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting");

            migrationBuilder.DropIndex(
                name: "IX_tenant_setting_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting");

            migrationBuilder.DropIndex(
                name: "IX_tenant_setting_TenantId1",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting");

            migrationBuilder.DropColumn(
                name: "TenantId1",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting");

            migrationBuilder.RenameTable(
                name: "user_role",
                schema: "YOUR_SCHEMA",
                newName: "User_Role",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "user_identity",
                schema: "YOUR_SCHEMA",
                newName: "User_Identity",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "YOUR_SCHEMA",
                newName: "User",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "system_log",
                schema: "YOUR_SCHEMA",
                newName: "System_Log",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "session",
                schema: "YOUR_SCHEMA",
                newName: "Session",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "role_permission",
                schema: "YOUR_SCHEMA",
                newName: "Role_Permission",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "role",
                schema: "YOUR_SCHEMA",
                newName: "Role",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "permission",
                schema: "YOUR_SCHEMA",
                newName: "Permission",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "password_history",
                schema: "YOUR_SCHEMA",
                newName: "Password_History",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "otp",
                schema: "YOUR_SCHEMA",
                newName: "Otp",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameTable(
                name: "tenant_setting",
                schema: "YOUR_SCHEMA",
                newName: "TenantSettingEntity",
                newSchema: "YOUR_SCHEMA");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_role_id",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "IX_User_Role_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_identity_user_id",
                schema: "YOUR_SCHEMA",
                table: "User_Identity",
                newName: "IX_User_Identity_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_session_user_id",
                schema: "YOUR_SCHEMA",
                table: "Session",
                newName: "IX_Session_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_session_refresh_token",
                schema: "YOUR_SCHEMA",
                table: "Session",
                newName: "IX_Session_refresh_token");

            migrationBuilder.RenameIndex(
                name: "IX_role_permission_role_id_permission_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "IX_Role_Permission_role_id_permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_permission_permission_id",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                newName: "IX_Role_Permission_permission_id");

            migrationBuilder.RenameIndex(
                name: "IX_role_name",
                schema: "YOUR_SCHEMA",
                table: "Role",
                newName: "IX_Role_name");

            migrationBuilder.RenameIndex(
                name: "IX_permission_code",
                schema: "YOUR_SCHEMA",
                table: "Permission",
                newName: "IX_Permission_code");

            migrationBuilder.RenameIndex(
                name: "IX_password_history_user_id",
                schema: "YOUR_SCHEMA",
                table: "Password_History",
                newName: "IX_Password_History_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_otp_user_id",
                schema: "YOUR_SCHEMA",
                table: "Otp",
                newName: "IX_Otp_user_id");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "support_url",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity",
                newName: "SupportUrl");

            migrationBuilder.RenameColumn(
                name: "support_email",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity",
                newName: "SupportEmail");

            migrationBuilder.RenameColumn(
                name: "logo_url",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity",
                newName: "LogoUrl");

            migrationBuilder.RenameColumn(
                name: "friendly_name",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity",
                newName: "FriendlyName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Role",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Identity",
                schema: "YOUR_SCHEMA",
                table: "User_Identity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_System_Log",
                schema: "YOUR_SCHEMA",
                table: "System_Log",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session",
                schema: "YOUR_SCHEMA",
                table: "Session",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role_Permission",
                schema: "YOUR_SCHEMA",
                table: "Role_Permission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "YOUR_SCHEMA",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                schema: "YOUR_SCHEMA",
                table: "Permission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Password_History",
                schema: "YOUR_SCHEMA",
                table: "Password_History",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Otp",
                schema: "YOUR_SCHEMA",
                table: "Otp",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenantSettingEntity",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSettingEntity_TenantId",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity",
                column: "TenantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Otp_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "Otp",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Password_History_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "Password_History",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Session_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "Session",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantSettingEntity_tenant_TenantId",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity",
                column: "TenantId",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Identity_User_user_id",
                schema: "YOUR_SCHEMA",
                table: "User_Identity",
                column: "user_id",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "User",
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
    }
}
