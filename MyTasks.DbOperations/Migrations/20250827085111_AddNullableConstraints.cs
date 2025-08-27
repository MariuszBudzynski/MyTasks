using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTasks.DbOperations.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_OwnerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComment_TaskItem_TaskItemId",
                table: "TaskComment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComment_User_AuthorId",
                table: "TaskComment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Project_ProjectId",
                table: "TaskItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "TaskItem",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskItemId",
                table: "TaskComment",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "TaskComment",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Project",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_OwnerId",
                table: "Project",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComment_TaskItem_TaskItemId",
                table: "TaskComment",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComment_User_AuthorId",
                table: "TaskComment",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Project_ProjectId",
                table: "TaskItem",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_OwnerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComment_TaskItem_TaskItemId",
                table: "TaskComment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComment_User_AuthorId",
                table: "TaskComment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskItem_Project_ProjectId",
                table: "TaskItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "TaskItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskItemId",
                table: "TaskComment",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "TaskComment",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_OwnerId",
                table: "Project",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComment_TaskItem_TaskItemId",
                table: "TaskComment",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComment_User_AuthorId",
                table: "TaskComment",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItem_Project_ProjectId",
                table: "TaskItem",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
