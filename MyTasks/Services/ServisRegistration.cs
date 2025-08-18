using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.DbOperations.Repository;

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
            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });
            builder.Services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.ConfigureFilter(new AutoValidateAntiforgeryTokenAttribute());
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));
        }
    }
}
