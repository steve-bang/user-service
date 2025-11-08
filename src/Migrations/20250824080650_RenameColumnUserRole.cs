using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedBy",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "assigned_by");

            migrationBuilder.RenameColumn(
                name: "AssignedAt",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "assigned_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "assigned_by",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "AssignedBy");

            migrationBuilder.RenameColumn(
                name: "assigned_at",
                schema: "YOUR_SCHEMA",
                table: "User_Role",
                newName: "AssignedAt");
        }
    }
}
