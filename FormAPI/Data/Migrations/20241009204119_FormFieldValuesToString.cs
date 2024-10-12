using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class FormFieldValuesToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("b51fd1a2-6405-4359-99f3-138c4f2a7880"), "A form to collect customer feedback.", "JKUSA" });

            migrationBuilder.InsertData(
                table: "formrecords",
                columns: new[] { "Id", "CreatedAt", "FormFieldValues", "FormId" },
                values: new object[] { new Guid("3a58224d-1c2b-4c69-a50d-b80232629682"), new DateTime(2024, 10, 9, 20, 41, 18, 662, DateTimeKind.Utc).AddTicks(6969), "{\"profileFirstName\":\"Nelly\"}", new Guid("b51fd1a2-6405-4359-99f3-138c4f2a7880") });

            migrationBuilder.InsertData(
                table: "pages",
                columns: new[] { "Id", "FormId" },
                values: new object[] { new Guid("8d5625f0-1757-44d0-bd19-222627f57142"), new Guid("b51fd1a2-6405-4359-99f3-138c4f2a7880") });

            migrationBuilder.InsertData(
                table: "formfields",
                columns: new[] { "InternalId", "Attributes", "FieldType", "Id", "Kind", "Name", "PageId", "Required", "Rules" },
                values: new object[] { new Guid("01512f04-f4bb-42b9-9d85-9cef1297fca9"), new Dictionary<string, object> { ["maxLength"] = 100 }, "profileId", "profileFirstName", "profile", "profileFirstName", new Guid("8d5625f0-1757-44d0-bd19-222627f57142"), true, new Dictionary<string, object> { ["minLength"] = 2, ["pattern"] = "^[a-zA-Z ]*$" } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "formfields",
                keyColumn: "InternalId",
                keyValue: new Guid("01512f04-f4bb-42b9-9d85-9cef1297fca9"));

            migrationBuilder.DeleteData(
                table: "formrecords",
                keyColumn: "Id",
                keyValue: new Guid("3a58224d-1c2b-4c69-a50d-b80232629682"));

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "Id",
                keyValue: new Guid("8d5625f0-1757-44d0-bd19-222627f57142"));

            migrationBuilder.DeleteData(
                table: "forms",
                keyColumn: "Id",
                keyValue: new Guid("b51fd1a2-6405-4359-99f3-138c4f2a7880"));

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
    }
}
