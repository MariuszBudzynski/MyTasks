using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyTasks.API;
using MyTasks.Common;
using MyTasks.Common.Interfaces;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.DbOperations.Repositories;
using MyTasks.Repositories.Interfaces.IDashboardRepository;
using MyTasks.Repositories.Interfaces.ILoginRepository;
using MyTasks.Repositories.Interfaces.IUserDataRepository;
using MyTasks.Repositories.Repositories.DashboardRepository;
using MyTasks.Repositories.Repositories.LoginRepository;
using MyTasks.Repositories.Repositories.UserDataRepository;
using System.Text;

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

            //JWT Autentication config
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        if (ctx.Request.Cookies.ContainsKey("AuthToken"))
                        {
                            ctx.Token = ctx.Request.Cookies["AuthToken"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            //global AntiforgeryToken aplication use
            builder.Services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.ConfigureFilter(new AutoValidateAntiforgeryTokenAttribute());
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlite($"Data Source={dbPath}"));
            builder.Services.AddHttpContextAccessor();

            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(UsersController).Assembly);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJwtHelper, JwtHelper>();
            builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<ILoginValidator, LoginValidator>();
            builder.Services.AddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));
        }
    }
}
