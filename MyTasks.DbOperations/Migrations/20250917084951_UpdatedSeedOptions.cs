using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyTasks.DbOperations.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "First project for admin", "Admin Project A" });

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "Description", "Name", "OwnerId" },
                values: new object[] { "Second project for admin", "Admin Project B", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "Description", "Name", "OwnerId" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), "First project for regular user", "Regular Project A", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Second project for regular user", "Regular Project B", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "TaskComment",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "TaskItemId" },
                values: new object[,]
                {
                    { new Guid("13131313-1313-1313-1313-131313131313"), new Guid("11111111-1111-1111-1111-111111111111"), "Comment for Admin Task A1", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("14141414-1414-1414-1414-141414141414"), new Guid("11111111-1111-1111-1111-111111111111"), "Comment for Admin Task A2", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") }
                });

            migrationBuilder.UpdateData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "AssignedUserId", "Description", "ProjectId", "Title" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Task for Admin Project A", new Guid("55555555-5555-5555-5555-555555555555"), "Admin Task A1" });

            migrationBuilder.UpdateData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "AssignedUserId", "Description", "ProjectId", "Title" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Another task for Admin Project A", new Guid("55555555-5555-5555-5555-555555555555"), "Admin Task A2" });

            migrationBuilder.InsertData(
                table: "TaskItem",
                columns: new[] { "Id", "AssignedUserId", "Description", "DueDate", "IsCompleted", "ProjectId", "Title" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), "Task for Admin Project B", null, false, new Guid("66666666-6666-6666-6666-666666666666"), "Admin Task B1" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), "Another task for Admin Project B", null, true, new Guid("66666666-6666-6666-6666-666666666666"), "Admin Task B2" }
                });

            migrationBuilder.InsertData(
                table: "TaskComment",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "TaskItemId" },
                values: new object[,]
                {
                    { new Guid("15151515-1515-1515-1515-151515151515"), new Guid("11111111-1111-1111-1111-111111111111"), "Comment for Admin Task B1", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("16161616-1616-1616-1616-161616161616"), new Guid("11111111-1111-1111-1111-111111111111"), "Comment for Admin Task B2", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") }
                });

            migrationBuilder.InsertData(
                table: "TaskItem",
                columns: new[] { "Id", "AssignedUserId", "Description", "DueDate", "IsCompleted", "ProjectId", "Title" },
                values: new object[,]
                {
                    { new Guid("12121212-1212-1212-1212-121212121212"), new Guid("22222222-2222-2222-2222-222222222222"), "Another task for Regular Project B", null, true, new Guid("88888888-8888-8888-8888-888888888888"), "Regular Task B2" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("22222222-2222-2222-2222-222222222222"), "Task for Regular Project A", null, false, new Guid("77777777-7777-7777-7777-777777777777"), "Regular Task A1" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("22222222-2222-2222-2222-222222222222"), "Another task for Regular Project A", null, true, new Guid("77777777-7777-7777-7777-777777777777"), "Regular Task A2" },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("22222222-2222-2222-2222-222222222222"), "Task for Regular Project B", null, false, new Guid("88888888-8888-8888-8888-888888888888"), "Regular Task B1" }
                });

            migrationBuilder.InsertData(
                table: "TaskComment",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "TaskItemId" },
                values: new object[,]
                {
                    { new Guid("17171717-1717-1717-1717-171717171717"), new Guid("22222222-2222-2222-2222-222222222222"), "Comment for Regular Task A1", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("18181818-1818-1818-1818-181818181818"), new Guid("22222222-2222-2222-2222-222222222222"), "Comment for Regular Task A2", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee") },
                    { new Guid("19191919-1919-1919-1919-191919191919"), new Guid("22222222-2222-2222-2222-222222222222"), "Comment for Regular Task B1", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff") },
                    { new Guid("20202020-2020-2020-2020-202020202020"), new Guid("22222222-2222-2222-2222-222222222222"), "Comment for Regular Task B2", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("12121212-1212-1212-1212-121212121212") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("13131313-1313-1313-1313-131313131313"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("14141414-1414-1414-1414-141414141414"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("15151515-1515-1515-1515-151515151515"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("16161616-1616-1616-1616-161616161616"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("17171717-1717-1717-1717-171717171717"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("18181818-1818-1818-1818-181818181818"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("19191919-1919-1919-1919-191919191919"));

            migrationBuilder.DeleteData(
                table: "TaskComment",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202020"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("12121212-1212-1212-1212-121212121212"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Project for admin user", "Admin Project" });

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "Description", "Name", "OwnerId" },
                values: new object[] { "Sample project for regular user", "Demo Project", new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.InsertData(
                table: "TaskComment",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "TaskItemId" },
                values: new object[,]
                {
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("22222222-2222-2222-2222-222222222222"), "Regular comment 1", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("22222222-2222-2222-2222-222222222222"), "Regular comment 2", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") }
                });

            migrationBuilder.UpdateData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "AssignedUserId", "Description", "ProjectId", "Title" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "First task for regular user", new Guid("66666666-6666-6666-6666-666666666666"), "Regular Task 1" });

            migrationBuilder.UpdateData(
                table: "TaskItem",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "AssignedUserId", "Description", "ProjectId", "Title" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), "Second task for regular user", new Guid("66666666-6666-6666-6666-666666666666"), "Regular Task 2" });

            migrationBuilder.InsertData(
                table: "TaskItem",
                columns: new[] { "Id", "AssignedUserId", "Description", "DueDate", "IsCompleted", "ProjectId", "Title" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new Guid("11111111-1111-1111-1111-111111111111"), "First task for admin", null, false, new Guid("55555555-5555-5555-5555-555555555555"), "Admin Task 1" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("11111111-1111-1111-1111-111111111111"), "Second task for admin", null, true, new Guid("55555555-5555-5555-5555-555555555555"), "Admin Task 2" }
                });

            migrationBuilder.InsertData(
                table: "TaskComment",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "TaskItemId" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), "Admin comment 1", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), "Admin comment 2", new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("88888888-8888-8888-8888-888888888888") }
                });
        }
    }
}
