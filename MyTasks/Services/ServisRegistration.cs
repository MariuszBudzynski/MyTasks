using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.DbOperations.Repository;
using MyTasks.Repositories.Interfaces.ILoginRepository;
using MyTasks.Repositories.Repositories.LoginRepository;
using MyTasks.Services.Interfaces;

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
            //global AntiforgeryToken aplication use
            builder.Services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.ConfigureFilter(new AutoValidateAntiforgeryTokenAttribute());
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<ILoginValidator, LoginValidator>();
            builder.Services.AddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));
        }
    }
}
