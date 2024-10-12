using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RowVersionThirdRun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ensure RowVersion is stored as a byte array but not treated as a concurrency token
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "forms",
                type: "bytea",
                nullable: false,
                defaultValue: Guid.NewGuid().ToByteArray()  // Ensuring default value
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
               name: "RowVersion",
               table: "forms",
               type: "bytea",
               nullable: true
           );
        }
    }
}
