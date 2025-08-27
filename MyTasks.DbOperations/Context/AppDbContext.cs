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

            modelBuilder.Entity<UserModel>()
           .HasOne(u => u.LoginModel)
           .WithOne(l => l.User)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectModel>()
               .HasOne(p => p.Owner)
               .WithMany(u => u.Projects)
               .HasForeignKey(p => p.OwnerId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskItemModel>()
               .HasOne(t => t.Project)
               .WithMany(p => p.Tasks)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskItemModel>()
               .HasOne(t => t.AssignedUser)
               .WithMany(u => u.AssignedTasks)
               .HasForeignKey(t => t.AssignedUserId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskCommentModel>()
               .HasOne(c => c.TaskItem)
               .WithMany(t => t.Comments)
               .HasForeignKey(c => c.TaskItemId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskCommentModel>()
               .HasOne(c => c.Author)
               .WithMany()
               .HasForeignKey(c => c.AuthorId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Seed();
        }

        public required DbSet<LoginModel> Login { get; set; }
        public required DbSet<ProjectModel> Project { get; set; }
        public required DbSet<TaskCommentModel> TaskComment { get; set; }
        public required DbSet<TaskItemModel> TaskItem { get; set; }
        public required DbSet<UserModel> User { get; set; }
    }
}
