using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnumsInFormFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "forms",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "forms",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "forms",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "forms",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

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
    }
}
