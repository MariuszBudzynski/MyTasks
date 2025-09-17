using Microsoft.EntityFrameworkCore;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Context
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var staticDate = new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc);

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
            var adminProject1Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var adminProject2Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var regularProject1Id = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var regularProject2Id = Guid.Parse("88888888-8888-8888-8888-888888888888");

            var adminProject1 = new ProjectModel { Id = adminProject1Id, Name = "Admin Project A", Description = "First project for admin", OwnerId = adminUserId };
            var adminProject2 = new ProjectModel { Id = adminProject2Id, Name = "Admin Project B", Description = "Second project for admin", OwnerId = adminUserId };
            var regularProject1 = new ProjectModel { Id = regularProject1Id, Name = "Regular Project A", Description = "First project for regular user", OwnerId = regularUserId };
            var regularProject2 = new ProjectModel { Id = regularProject2Id, Name = "Regular Project B", Description = "Second project for regular user", OwnerId = regularUserId };

            modelBuilder.Entity<ProjectModel>().HasData(adminProject1, adminProject2, regularProject1, regularProject2);

            // --- TASKS ---
            var task1Id = Guid.Parse("99999999-9999-9999-9999-999999999999");
            var task2Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var task3Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var task4Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var task5Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
            var task6Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
            var task7Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            var task8Id = Guid.Parse("12121212-1212-1212-1212-121212121212");

            var task1 = new TaskItemModel { Id = task1Id, Title = "Admin Task A1", Description = "Task for Admin Project A", IsCompleted = false, ProjectId = adminProject1Id, AssignedUserId = adminUserId };
            var task2 = new TaskItemModel { Id = task2Id, Title = "Admin Task A2", Description = "Another task for Admin Project A", IsCompleted = true, ProjectId = adminProject1Id, AssignedUserId = adminUserId };
            var task3 = new TaskItemModel { Id = task3Id, Title = "Admin Task B1", Description = "Task for Admin Project B", IsCompleted = false, ProjectId = adminProject2Id, AssignedUserId = adminUserId };
            var task4 = new TaskItemModel { Id = task4Id, Title = "Admin Task B2", Description = "Another task for Admin Project B", IsCompleted = true, ProjectId = adminProject2Id, AssignedUserId = adminUserId };
            var task5 = new TaskItemModel { Id = task5Id, Title = "Regular Task A1", Description = "Task for Regular Project A", IsCompleted = false, ProjectId = regularProject1Id, AssignedUserId = regularUserId };
            var task6 = new TaskItemModel { Id = task6Id, Title = "Regular Task A2", Description = "Another task for Regular Project A", IsCompleted = true, ProjectId = regularProject1Id, AssignedUserId = regularUserId };
            var task7 = new TaskItemModel { Id = task7Id, Title = "Regular Task B1", Description = "Task for Regular Project B", IsCompleted = false, ProjectId = regularProject2Id, AssignedUserId = regularUserId };
            var task8 = new TaskItemModel { Id = task8Id, Title = "Regular Task B2", Description = "Another task for Regular Project B", IsCompleted = true, ProjectId = regularProject2Id, AssignedUserId = regularUserId };

            modelBuilder.Entity<TaskItemModel>().HasData(task1, task2, task3, task4, task5, task6, task7, task8);

            // --- COMMENTS ---
            var comment1Id = Guid.Parse("13131313-1313-1313-1313-131313131313");
            var comment2Id = Guid.Parse("14141414-1414-1414-1414-141414141414");
            var comment3Id = Guid.Parse("15151515-1515-1515-1515-151515151515");
            var comment4Id = Guid.Parse("16161616-1616-1616-1616-161616161616");
            var comment5Id = Guid.Parse("17171717-1717-1717-1717-171717171717");
            var comment6Id = Guid.Parse("18181818-1818-1818-1818-181818181818");
            var comment7Id = Guid.Parse("19191919-1919-1919-1919-191919191919");
            var comment8Id = Guid.Parse("20202020-2020-2020-2020-202020202020");

            var comment1 = new TaskCommentModel { Id = comment1Id, Content = "Comment for Admin Task A1", CreatedAt = staticDate, TaskItemId = task1Id, AuthorId = adminUserId };
            var comment2 = new TaskCommentModel { Id = comment2Id, Content = "Comment for Admin Task A2", CreatedAt = staticDate, TaskItemId = task2Id, AuthorId = adminUserId };
            var comment3 = new TaskCommentModel { Id = comment3Id, Content = "Comment for Admin Task B1", CreatedAt = staticDate, TaskItemId = task3Id, AuthorId = adminUserId };
            var comment4 = new TaskCommentModel { Id = comment4Id, Content = "Comment for Admin Task B2", CreatedAt = staticDate, TaskItemId = task4Id, AuthorId = adminUserId };
            var comment5 = new TaskCommentModel { Id = comment5Id, Content = "Comment for Regular Task A1", CreatedAt = staticDate, TaskItemId = task5Id, AuthorId = regularUserId };
            var comment6 = new TaskCommentModel { Id = comment6Id, Content = "Comment for Regular Task A2", CreatedAt = staticDate, TaskItemId = task6Id, AuthorId = regularUserId };
            var comment7 = new TaskCommentModel { Id = comment7Id, Content = "Comment for Regular Task B1", CreatedAt = staticDate, TaskItemId = task7Id, AuthorId = regularUserId };
            var comment8 = new TaskCommentModel { Id = comment8Id, Content = "Comment for Regular Task B2", CreatedAt = staticDate, TaskItemId = task8Id, AuthorId = regularUserId };

            modelBuilder.Entity<TaskCommentModel>().HasData(comment1, comment2, comment3, comment4, comment5, comment6, comment7, comment8);
        }
    }
}