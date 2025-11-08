using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tenant",
                schema: "YOUR_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    domain = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    branding = table.Column<string>(type: "text", nullable: false),
                    metadata = table.Column<string>(type: "text", nullable: false),
                    trials_end_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    subscription_end_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "custom_domain",
                schema: "YOUR_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    domain = table.Column<string>(type: "text", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    is_verified = table.Column<bool>(type: "boolean", nullable: false),
                    verified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    verification_method = table.Column<string>(type: "text", nullable: false),
                    verification_token = table.Column<string>(type: "text", nullable: false),
                    verification_record = table.Column<string>(type: "text", nullable: false),
                    ssl_certificate_id = table.Column<string>(type: "text", nullable: false),
                    ssl_status = table.Column<short>(type: "smallint", nullable: false),
                    ssl_expired_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_custom_domain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_custom_domain_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalSchema: "YOUR_SCHEMA",
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenant_address",
                schema: "YOUR_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    line_1 = table.Column<string>(type: "text", nullable: false),
                    line_2 = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false),
                    postal_code = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tenant_address_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalSchema: "YOUR_SCHEMA",
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenant_policy",
                schema: "YOUR_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    metadata = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_policy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tenant_policy_tenant_tenant_id",
                        column: x => x.tenant_id,
                        principalSchema: "YOUR_SCHEMA",
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantSettingEntity",
                schema: "YOUR_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    FriendlyName = table.Column<string>(type: "text", nullable: true),
                    LogoUrl = table.Column<string>(type: "text", nullable: true),
                    SupportEmail = table.Column<string>(type: "text", nullable: true),
                    SupportUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSettingEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSettingEntity_tenant_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "YOUR_SCHEMA",
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_custom_domain_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "custom_domain",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_address_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "tenant_address",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_policy_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "tenant_policy",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSettingEntity_TenantId",
                schema: "YOUR_SCHEMA",
                table: "TenantSettingEntity",
                column: "TenantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "custom_domain",
                schema: "YOUR_SCHEMA");

            migrationBuilder.DropTable(
                name: "tenant_address",
                schema: "YOUR_SCHEMA");

            migrationBuilder.DropTable(
                name: "tenant_policy",
                schema: "YOUR_SCHEMA");

            migrationBuilder.DropTable(
                name: "TenantSettingEntity",
                schema: "YOUR_SCHEMA");

            migrationBuilder.DropTable(
                name: "tenant",
                schema: "YOUR_SCHEMA");
        }
    }
}
