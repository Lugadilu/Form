using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "formrecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormFieldValues = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formrecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_formrecords_forms_FormId",
                        column: x => x.FormId,
                        principalTable: "forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pages_forms_FormId",
                        column: x => x.FormId,
                        principalTable: "forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "formfields",
                columns: table => new
                {
                    InternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Attributes = table.Column<string>(type: "jsonb", nullable: false),
                    Kind = table.Column<string>(type: "text", nullable: false),
                    FieldType = table.Column<string>(type: "text", nullable: false),
                    Rules = table.Column<string>(type: "jsonb", nullable: false),
                    PageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_formfields", x => x.InternalId);
                    table.ForeignKey(
                        name: "FK_formfields_pages_PageId",
                        column: x => x.PageId,
                        principalTable: "pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "forms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("e1b2ab67-45d2-456b-ac48-c2b59f1e84ba"), "A form to collect customer feedback.", "Customer Feedback Form" });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "CreatedAt", "FormFieldValues", "FormId" },
                values: new object[] { new Guid("c3be4411-32a1-4091-b882-ed0562943679"), new DateTime(2024, 10, 4, 8, 0, 53, 577, DateTimeKind.Utc).AddTicks(2804), new Dictionary<string, object> { ["profileFirstName"] = "Nelly" }, new Guid("e1b2ab67-45d2-456b-ac48-c2b59f1e84ba") });

            migrationBuilder.InsertData(
                table: "pages",
                columns: new[] { "Id", "FormId" },
                values: new object[] { new Guid("34a0d875-1e9e-4c30-afd5-383ce6257937"), new Guid("e1b2ab67-45d2-456b-ac48-c2b59f1e84ba") });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "InternalId", "Attributes", "FieldType", "Id", "Kind", "Name", "PageId", "Required", "Rules" },
                values: new object[] { new Guid("ce8b08ce-8972-4ad8-8e87-a6861d099092"), "{\"maxLength\":100}", "profileId", "profileFirstName", "profile", "profileFirstName", new Guid("34a0d875-1e9e-4c30-afd5-383ce6257937"), true, "{\"minLength\":2,\"pattern\":\"^[a-zA-Z ]*$\"}" });

            migrationBuilder.CreateIndex(
                name: "IX_formfields_PageId",
                table: "formfields",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_formrecords_FormId",
                table: "formrecords",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_pages_FormId",
                table: "pages",
                column: "FormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "formfields");

            migrationBuilder.DropTable(
                name: "formrecords");

            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "forms");
        }
    }
}
