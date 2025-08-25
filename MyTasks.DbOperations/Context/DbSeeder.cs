using Microsoft.EntityFrameworkCore;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Context
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var staticDate = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);

            // --- USERS ---
            var adminUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var regularUserId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var adminLoginId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var regularLoginId = Guid.Parse("44444444-4444-4444-4444-444444444444");

            var adminUser = new UserModel { Id = adminUserId, FullName = "Admin User", LoginId = adminLoginId };
            var regularUser = new UserModel { Id = regularUserId, FullName = "Regular User", LoginId = regularLoginId };

            var adminLogin = new LoginModel { Id = adminLoginId, Username = "admin", PasswordHash = "admin123", Type = UserType.Admin, UserId = adminUserId, FakeUser = true };
            var regularLogin = new LoginModel { Id = regularLoginId, Username = "user", PasswordHash = "user123", Type = UserType.Regular, UserId = regularUserId, FakeUser = true };

            modelBuilder.Entity<UserModel>().HasData(adminUser, regularUser);
            modelBuilder.Entity<LoginModel>().HasData(adminLogin, regularLogin);

            // --- PROJECTS ---
            var adminProjectId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var regularProjectId = Guid.Parse("66666666-6666-6666-6666-666666666666");

            var adminProject = new ProjectModel
            {
                Id = adminProjectId,
                Name = "Admin Project",
                Description = "Project for admin user",
                OwnerId = adminUserId
            };

            var regularProject = new ProjectModel
            {
                Id = regularProjectId,
                Name = "Demo Project",
                Description = "Sample project for regular user",
                OwnerId = regularUserId
            };

            modelBuilder.Entity<ProjectModel>().HasData(adminProject, regularProject);

            // --- TASKS ---
            var task1Id = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var task2Id = Guid.Parse("88888888-8888-8888-8888-888888888888");
            var task3Id = Guid.Parse("99999999-9999-9999-9999-999999999999");
            var task4Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            var task1 = new TaskItemModel
            {
                Id = task1Id,
                Title = "Admin Task 1",
                Description = "First task for admin",
                IsCompleted = false,
                ProjectId = adminProjectId,
                AssignedUserId = adminUserId
            };

            var task2 = new TaskItemModel
            {
                Id = task2Id,
                Title = "Admin Task 2",
                Description = "Second task for admin",
                IsCompleted = true,
                ProjectId = adminProjectId,
                AssignedUserId = adminUserId
            };

            var task3 = new TaskItemModel
            {
                Id = task3Id,
                Title = "Regular Task 1",
                Description = "First task for regular user",
                IsCompleted = false,
                ProjectId = regularProjectId,
                AssignedUserId = regularUserId
            };

            var task4 = new TaskItemModel
            {
                Id = task4Id,
                Title = "Regular Task 2",
                Description = "Second task for regular user",
                IsCompleted = true,
                ProjectId = regularProjectId,
                AssignedUserId = regularUserId
            };

            modelBuilder.Entity<TaskItemModel>().HasData(task1, task2, task3, task4);

            // --- COMMENTS ---
            var comment1Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var comment2Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var comment3Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
            var comment4Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            var comment1 = new TaskCommentModel { Id = comment1Id, Content = "Admin comment 1", CreatedAt = staticDate, TaskItemId = task1Id, AuthorId = adminUserId };
            var comment2 = new TaskCommentModel { Id = comment2Id, Content = "Admin comment 2", CreatedAt = staticDate, TaskItemId = task2Id, AuthorId = adminUserId };
            var comment3 = new TaskCommentModel { Id = comment3Id, Content = "Regular comment 1", CreatedAt = staticDate, TaskItemId = task3Id, AuthorId = regularUserId };
            var comment4 = new TaskCommentModel { Id = comment4Id, Content = "Regular comment 2", CreatedAt = staticDate, TaskItemId = task4Id, AuthorId = regularUserId };

            modelBuilder.Entity<TaskCommentModel>().HasData(comment1, comment2, comment3, comment4);
        }
    }
}