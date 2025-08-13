using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Context;

namespace MyTasks.Services
{
    public static class ServisRegistration
    {
        public static void Register(this WebApplicationBuilder builder)
        {
            var relativePath = builder.Configuration.GetConnectionString("DefaultConnection");
            var projectFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            var dbPath = Path.Combine(projectFolder, relativePath);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlite($"Data Source={dbPath}"));
        }
    }
}
