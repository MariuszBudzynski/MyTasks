using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyTasks.DbOperations.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FullName", "LoginId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Admin User", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Regular User", new Guid("44444444-4444-4444-4444-444444444444") }
                });

            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "Id", "PasswordHash", "Type", "UserId", "Username" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "admin123", 0, new Guid("11111111-1111-1111-1111-111111111111"), "admin" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "user123", 1, new Guid("22222222-2222-2222-2222-222222222222"), "user" }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Project for admin user", "Admin Project", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Sample project for regular user", "Demo Project", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "TaskItem",
                columns: new[] { "Id", "AssignedUserId", "Description", "DueDate", "IsCompleted", "ProjectId", "Title" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("11111111-1111-1111-1111-111111111111"), "First task for admin", null, false, new Guid("55555555-5555-5555-5555-555555555555"), "Admin Task 1" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("11111111-1111-1111-1111-111111111111"), "Second task for admin", null, true, new Guid("55555555-5555-5555-5555-555555555555"), "Admin Task 2" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new Guid("22222222-2222-2222-2222-222222222222"), "First task for regular user", null, false, new Guid("66666666-6666-6666-6666-666666666666"), "Regular Task 1" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("22222222-2222-2222-2222-222222222222"), "Second task for regular user", null, true, new Guid("66666666-6666-6666-6666-666666666666"), "Regular Task 2" }
                });

            migrationBuilder.InsertData(
                table: "TaskComment",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "TaskItemId" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), "Admin comment 1", new DateTime(2025, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), "Admin comment 2", new DateTime(2025, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("22222222-2222-2222-2222-222222222222"), "Regular comment 1", new DateTime(2025, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("22222222-2222-2222-2222-222222222222"), "Regular comment 2", new DateTime(2025, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Login",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Login",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}
