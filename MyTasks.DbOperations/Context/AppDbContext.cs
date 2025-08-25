using Microsoft.EntityFrameworkCore;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();
        }

        public required DbSet<LoginModel> Login { get; set; }
        public required DbSet<ProjectModel> Project { get; set; }
        public required DbSet<TaskCommentModel> TaskComment { get; set; }
        public required DbSet<TaskItemModel> TaskItem { get; set; }
        public required DbSet<UserModel> User { get; set; }
    }
}
