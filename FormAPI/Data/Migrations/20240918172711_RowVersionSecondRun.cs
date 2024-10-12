using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RowVersionSecondRun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "forms",
                type: "bytea", // In PostgreSQL, use bytea for binary data like row version
                rowVersion: true,  // Enable this as a row version for concurrency handling
                nullable: false,
                defaultValueSql: "gen_random_uuid()::bytea"  // Set the default to an empty byte array
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the RowVersion column if the migration is rolled back
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "forms"
            );
        }
    }
}
