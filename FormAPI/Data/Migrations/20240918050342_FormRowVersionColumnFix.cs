using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class FormRowVersionColumnFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Alter the RowVersion column to make it nullable
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "forms",
                type: "bytea",
                nullable: true, // Set nullable to true
                oldClrType: typeof(byte[]),
                oldType: "bytea");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert the RowVersion column to be not nullable (in case of rollback)
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "forms",
                type: "bytea",
                nullable: false, // Set nullable to false (default behavior)
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }
    }
}
