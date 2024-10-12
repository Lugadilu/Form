using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedJsonStringConvertor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "InternalId",
                keyValue: new Guid("ce8b08ce-8972-4ad8-8e87-a6861d099092"));

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: new Guid("c3be4411-32a1-4091-b882-ed0562943679"));

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "Id",
                keyValue: new Guid("34a0d875-1e9e-4c30-afd5-383ce6257937"));

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: new Guid("e1b2ab67-45d2-456b-ac48-c2b59f1e84ba"));

            migrationBuilder.InsertData(
                table: "forms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("42f6dc53-d5cd-47e0-a75a-bd7ecb0b0d63"), "A form to collect customer feedback.", "Customer Feedback Form" });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "CreatedAt", "FormFieldValues", "FormId" },
                values: new object[] { new Guid("d33b31ae-917b-4609-9960-bfea62dbccf8"), new DateTime(2024, 10, 8, 5, 38, 53, 828, DateTimeKind.Utc).AddTicks(4430), new Dictionary<string, object> { ["profileFirstName"] = "Nelly" }, new Guid("42f6dc53-d5cd-47e0-a75a-bd7ecb0b0d63") });

            migrationBuilder.InsertData(
                table: "pages",
                columns: new[] { "Id", "FormId" },
                values: new object[] { new Guid("ec0acf3b-7dca-4cc4-b575-c75ce7f2e2ae"), new Guid("42f6dc53-d5cd-47e0-a75a-bd7ecb0b0d63") });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "InternalId", "Attributes", "FieldType", "Id", "Kind", "Name", "PageId", "Required", "Rules" },
                values: new object[] { new Guid("919f6a52-8e33-4825-9226-e2b034693c5c"), new Dictionary<string, object> { ["maxLength"] = 100 }, "profileId", "profileFirstName", "profile", "profileFirstName", new Guid("ec0acf3b-7dca-4cc4-b575-c75ce7f2e2ae"), true, new Dictionary<string, object> { ["minLength"] = 2, ["pattern"] = "^[a-zA-Z ]*$" } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "InternalId",
                keyValue: new Guid("919f6a52-8e33-4825-9226-e2b034693c5c"));

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: new Guid("d33b31ae-917b-4609-9960-bfea62dbccf8"));

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "Id",
                keyValue: new Guid("ec0acf3b-7dca-4cc4-b575-c75ce7f2e2ae"));

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: new Guid("42f6dc53-d5cd-47e0-a75a-bd7ecb0b0d63"));

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
        }
    }
}
