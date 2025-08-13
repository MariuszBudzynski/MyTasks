using Microsoft.EntityFrameworkCore;
using MyTasks.Models.Models;

namespace MyTasks.Db.Operations.Context
{
    public class AppDbContext : DbContext
    {

        private readonly string _dbPath;

        public AppDbContext()
        {
            var projectFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "MyDataLibrary"));
            _dbPath = Path.Combine(projectFolder, "Database.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlite($"Data Source={_dbPath}");

        public required DbSet<LoginModel> Login { get; set; }
        public required DbSet<ProjectModel> Project { get; set; }
        public required DbSet<TaskCommentModel> TaskComment { get; set; }
        public required DbSet<TaskItemModel> TaskItem { get; set; }
        public required DbSet<UserModel> User { get; set; }
    }
}
