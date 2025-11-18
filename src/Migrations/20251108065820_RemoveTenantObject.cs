using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTenantObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "tenant_setting",
                schema: "YOUR_SCHEMA");

            migrationBuilder.DropTable(
                name: "tenant",
                schema: "YOUR_SCHEMA");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                schema: "YOUR_SCHEMA",
                table: "user",
                newName: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "YOUR_SCHEMA",
                table: "user",
                newName: "tenant_id");

            migrationBuilder.CreateTable(
                name: "tenant",
                schema: "YOUR_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    branding = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    description = table.Column<string>(type: "text", nullable: false),
                    domain = table.Column<string>(type: "text", nullable: false),
                    metadata = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    subscription_end_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    trials_end_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    domain = table.Column<string>(type: "text", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    is_verified = table.Column<bool>(type: "boolean", nullable: false),
                    ssl_certificate_id = table.Column<string>(type: "text", nullable: false),
                    ssl_expired_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ssl_status = table.Column<short>(type: "smallint", nullable: false),
                    verification_method = table.Column<string>(type: "text", nullable: false),
                    verification_record = table.Column<string>(type: "text", nullable: false),
                    verification_token = table.Column<string>(type: "text", nullable: false),
                    verified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    city = table.Column<string>(type: "text", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    line_1 = table.Column<string>(type: "text", nullable: false),
                    line_2 = table.Column<string>(type: "text", nullable: true),
                    postal_code = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    metadata = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                name: "tenant_setting",
                schema: "YOUR_SCHEMA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    friendly_name = table.Column<string>(type: "text", nullable: true),
                    logo_url = table.Column<string>(type: "text", nullable: true),
                    support_email = table.Column<string>(type: "text", nullable: true),
                    support_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_setting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tenant_setting_tenant_tenant_id",
                        column: x => x.tenant_id,
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
                name: "ix_domain",
                schema: "YOUR_SCHEMA",
                table: "tenant",
                column: "domain");

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
                name: "IX_tenant_setting_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "tenant_setting",
                column: "tenant_id",
                unique: true);
        }
    }
}
