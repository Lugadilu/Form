using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class NameUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("674192a3-6aeb-4a53-a084-61aeb87e3a6e"), "A form to collect customer feedback.", "JKUSA" });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "CreatedAt", "FormFieldValues", "FormId" },
                values: new object[] { new Guid("656c5cf1-a9aa-45e9-9d57-4714490ed56f"), new DateTime(2024, 10, 9, 11, 17, 30, 441, DateTimeKind.Utc).AddTicks(3882), new Dictionary<string, object> { ["profileFirstName"] = "Nelly" }, new Guid("674192a3-6aeb-4a53-a084-61aeb87e3a6e") });

            migrationBuilder.InsertData(
                table: "pages",
                columns: new[] { "Id", "FormId" },
                values: new object[] { new Guid("c6045c53-3ece-4973-97a6-34a9a589fe1c"), new Guid("674192a3-6aeb-4a53-a084-61aeb87e3a6e") });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "InternalId", "Attributes", "FieldType", "Id", "Kind", "Name", "PageId", "Required", "Rules" },
                values: new object[] { new Guid("5a3f3b01-49ed-450b-b274-75cab3a40140"), new Dictionary<string, object> { ["maxLength"] = 100 }, "profileId", "profileFirstName", "profile", "profileFirstName", new Guid("c6045c53-3ece-4973-97a6-34a9a589fe1c"), true, new Dictionary<string, object> { ["minLength"] = 2, ["pattern"] = "^[a-zA-Z ]*$" } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "InternalId",
                keyValue: new Guid("5a3f3b01-49ed-450b-b274-75cab3a40140"));

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: new Guid("656c5cf1-a9aa-45e9-9d57-4714490ed56f"));

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "Id",
                keyValue: new Guid("c6045c53-3ece-4973-97a6-34a9a589fe1c"));

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: new Guid("674192a3-6aeb-4a53-a084-61aeb87e3a6e"));

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
    }
}
