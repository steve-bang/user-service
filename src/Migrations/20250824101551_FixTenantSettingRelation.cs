using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class FixTenantSettingRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tenant_setting_tenant_TenantId1",
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

            migrationBuilder.CreateIndex(
                name: "IX_tenant_setting_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                column: "tenant_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tenant_setting_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId1",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                type: "uuid",
                nullable: true);

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
                name: "FK_tenant_setting_tenant_TenantId1",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                column: "TenantId1",
                principalSchema: "YOUR_SCHEMA",
                principalTable: "tenant",
                principalColumn: "Id");
        }
    }
}
