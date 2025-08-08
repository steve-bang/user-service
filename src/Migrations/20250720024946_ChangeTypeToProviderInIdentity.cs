using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeToProviderInIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "YOUR_SCHEMA",
                table: "User_Identity");

            migrationBuilder.AddColumn<string>(
                name: "provider",
                schema: "YOUR_SCHEMA",
                table: "User_Identity",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "provider",
                schema: "YOUR_SCHEMA",
                table: "User_Identity");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "YOUR_SCHEMA",
                table: "User_Identity",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
