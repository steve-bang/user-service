using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnsConfigOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "YOUR_SCHEMA",
                table: "Otp",
                newName: "type");

            migrationBuilder.AlterColumn<int>(
                name: "retry_count",
                schema: "YOUR_SCHEMA",
                table: "Otp",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<bool>(
                name: "is_used",
                schema: "YOUR_SCHEMA",
                table: "Otp",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                schema: "YOUR_SCHEMA",
                table: "Otp",
                newName: "Type");

            migrationBuilder.AlterColumn<int>(
                name: "retry_count",
                schema: "YOUR_SCHEMA",
                table: "Otp",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "is_used",
                schema: "YOUR_SCHEMA",
                table: "Otp",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);
        }
    }
}
