using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class RenameUpdateAtColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                schema: "YOUR_SCHEMA",
                table: "tenant_address",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                schema: "YOUR_SCHEMA",
                table: "tenant",
                newName: "updated_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_at",
                schema: "YOUR_SCHEMA",
                table: "tenant_address",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                schema: "YOUR_SCHEMA",
                table: "tenant",
                newName: "UpdatedAt");
        }
    }
}
