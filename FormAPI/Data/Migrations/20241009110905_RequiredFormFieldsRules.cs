using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RequiredFormFieldsRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "InternalId",
                keyValue: new Guid("4693b7df-8422-41e9-ae1b-dabbb27d467b"));

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: new Guid("5440eefa-d967-4bfe-9460-b6408e50f7d1"));

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "Id",
                keyValue: new Guid("ec9282a6-718e-42ea-8a9d-f339626ad3e8"));

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: new Guid("33000eae-886e-4bb5-937e-2971d7bb5d58"));

            migrationBuilder.InsertData(
                table: "forms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("69eaf2fb-a253-4cfe-82fe-44f7e6bc2a5b"), "A form to collect customer feedback.", "Customer Feedback Form" });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "CreatedAt", "FormFieldValues", "FormId" },
                values: new object[] { new Guid("4010cabe-5d8f-4f40-80a1-abb5852a63a7"), new DateTime(2024, 10, 9, 11, 9, 0, 353, DateTimeKind.Utc).AddTicks(838), new Dictionary<string, object> { ["profileFirstName"] = "Nelly" }, new Guid("69eaf2fb-a253-4cfe-82fe-44f7e6bc2a5b") });

            migrationBuilder.InsertData(
                table: "pages",
                columns: new[] { "Id", "FormId" },
                values: new object[] { new Guid("40725921-db65-4b5c-b13a-ddf25552d9a7"), new Guid("69eaf2fb-a253-4cfe-82fe-44f7e6bc2a5b") });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "InternalId", "Attributes", "FieldType", "Id", "Kind", "Name", "PageId", "Required", "Rules" },
                values: new object[] { new Guid("c974647d-bfb6-41cb-932e-2cdf1d1af304"), new Dictionary<string, object> { ["maxLength"] = 100 }, "profileId", "profileFirstName", "profile", "profileFirstName", new Guid("40725921-db65-4b5c-b13a-ddf25552d9a7"), true, new Dictionary<string, object> { ["minLength"] = 2, ["pattern"] = "^[a-zA-Z ]*$" } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "InternalId",
                keyValue: new Guid("c974647d-bfb6-41cb-932e-2cdf1d1af304"));

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: new Guid("4010cabe-5d8f-4f40-80a1-abb5852a63a7"));

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "Id",
                keyValue: new Guid("40725921-db65-4b5c-b13a-ddf25552d9a7"));

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: new Guid("69eaf2fb-a253-4cfe-82fe-44f7e6bc2a5b"));

            migrationBuilder.InsertData(
                table: "forms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("33000eae-886e-4bb5-937e-2971d7bb5d58"), "A form to collect customer feedback.", "Customer Feedback Form" });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "CreatedAt", "FormFieldValues", "FormId" },
                values: new object[] { new Guid("5440eefa-d967-4bfe-9460-b6408e50f7d1"), new DateTime(2024, 10, 8, 20, 37, 31, 437, DateTimeKind.Utc).AddTicks(4308), new Dictionary<string, object> { ["profileFirstName"] = "Nelly" }, new Guid("33000eae-886e-4bb5-937e-2971d7bb5d58") });

            migrationBuilder.InsertData(
                table: "pages",
                columns: new[] { "Id", "FormId" },
                values: new object[] { new Guid("ec9282a6-718e-42ea-8a9d-f339626ad3e8"), new Guid("33000eae-886e-4bb5-937e-2971d7bb5d58") });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "InternalId", "Attributes", "FieldType", "Id", "Kind", "Name", "PageId", "Required", "Rules" },
                values: new object[] { new Guid("4693b7df-8422-41e9-ae1b-dabbb27d467b"), new Dictionary<string, object> { ["maxLength"] = 100 }, "profileId", "profileFirstName", "profile", "profileFirstName", new Guid("ec9282a6-718e-42ea-8a9d-f339626ad3e8"), true, new Dictionary<string, object> { ["minLength"] = 2, ["pattern"] = "^[a-zA-Z ]*$" } });
        }
    }
}
