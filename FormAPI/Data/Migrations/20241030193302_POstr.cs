using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class POstr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "InternalId",
                keyValue: new Guid("93d20524-4b26-4de0-a509-2b104bcc4970"));

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: new Guid("1cb4833d-41ee-4fe7-b077-6e1982c41202"));

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "Id",
                keyValue: new Guid("dd2b6d09-5dfb-4b7b-8803-0f2248e0bd2d"));

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: new Guid("ec0468f6-8a0c-46ea-94b8-3f547074c42c"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "forms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("ec0468f6-8a0c-46ea-94b8-3f547074c42c"), "A form to collect customer feedback.", "JKUSA" });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "CreatedAt", "FormFieldValues", "FormId" },
                values: new object[] { new Guid("1cb4833d-41ee-4fe7-b077-6e1982c41202"), new DateTime(2024, 10, 10, 10, 16, 54, 82, DateTimeKind.Utc).AddTicks(9915), "{\"profileFirstName\":\"Nelly\"}", new Guid("ec0468f6-8a0c-46ea-94b8-3f547074c42c") });

            migrationBuilder.InsertData(
                table: "pages",
                columns: new[] { "Id", "FormId" },
                values: new object[] { new Guid("dd2b6d09-5dfb-4b7b-8803-0f2248e0bd2d"), new Guid("ec0468f6-8a0c-46ea-94b8-3f547074c42c") });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "InternalId", "Attributes", "FieldType", "Id", "Kind", "Name", "PageId", "Required", "Rules" },
                values: new object[] { new Guid("93d20524-4b26-4de0-a509-2b104bcc4970"), new Dictionary<string, object>(), "FirstName", "profileFirstName", "profile", "profileFirstName", new Guid("dd2b6d09-5dfb-4b7b-8803-0f2248e0bd2d"), true, new Dictionary<string, object>() });
        }
    }
}
