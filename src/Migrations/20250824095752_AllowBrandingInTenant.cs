using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class AllowBrandingInTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_display_name",
                schema: "YOUR_SCHEMA",
                table: "user");

            migrationBuilder.AddColumn<Guid>(
                name: "tenant_id",
                schema: "YOUR_SCHEMA",
                table: "user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "metadata",
                schema: "YOUR_SCHEMA",
                table: "tenant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "branding",
                schema: "YOUR_SCHEMA",
                table: "tenant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_users_phone_number",
                schema: "YOUR_SCHEMA",
                table: "user",
                column: "phone_number");

            migrationBuilder.CreateIndex(
                name: "ix_users_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "user",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_domain",
                schema: "YOUR_SCHEMA",
                table: "tenant",
                column: "domain");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_phone_number",
                schema: "YOUR_SCHEMA",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_users_tenant_id",
                schema: "YOUR_SCHEMA",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_domain",
                schema: "YOUR_SCHEMA",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                schema: "YOUR_SCHEMA",
                table: "user");

            migrationBuilder.AlterColumn<string>(
                name: "metadata",
                schema: "YOUR_SCHEMA",
                table: "tenant",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "branding",
                schema: "YOUR_SCHEMA",
                table: "tenant",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_display_name",
                schema: "YOUR_SCHEMA",
                table: "user",
                column: "display_name");
        }
    }
}
