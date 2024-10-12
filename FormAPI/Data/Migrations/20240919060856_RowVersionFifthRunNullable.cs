﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RowVersionFifthRunNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "forms",
                type: "bytea",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                 oldNullable: false,
                oldRowVersion: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "forms",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldNullable: true);
        }
    }
}
