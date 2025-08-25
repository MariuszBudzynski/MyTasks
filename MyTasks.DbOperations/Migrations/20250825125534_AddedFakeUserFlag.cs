using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTasks.DbOperations.Migrations
{
    /// <inheritdoc />
    public partial class AddedFakeUserFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FakeUser",
                table: "Login",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Login",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "FakeUser",
                value: true);

            migrationBuilder.UpdateData(
                table: "Login",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "FakeUser",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FakeUser",
                table: "Login");
        }
    }
}
