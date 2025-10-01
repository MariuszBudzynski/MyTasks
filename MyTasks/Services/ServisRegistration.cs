using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyTasks.API;
using MyTasks.API.Services;
using MyTasks.API.Services.Interfaces;
using MyTasks.Common;
using MyTasks.Common.Interfaces;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.InMemory;
using MyTasks.DbOperations.Interface;
using MyTasks.DbOperations.Repositories;
using MyTasks.Repositories.Interfaces.IDashboardRepository;
using MyTasks.Repositories.Interfaces.ILoginRepository;
using MyTasks.Repositories.Interfaces.IProjecRepository;
using MyTasks.Repositories.Interfaces.ITaskCommentRepository;
using MyTasks.Repositories.Interfaces.ITaskItemRepository;
using MyTasks.Repositories.Interfaces.IUserDataRepository;
using MyTasks.Repositories.Repositories.DashboardRepository;
using MyTasks.Repositories.Repositories.LoginRepository;
using MyTasks.Repositories.Repositories.ProjecRepository;
using MyTasks.Repositories.Repositories.TaskCommentRepository;
using MyTasks.Repositories.Repositories.TaskItemRepository;
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
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
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
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/Login");
                        return Task.CompletedTask;
                    },
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
                    options.Conventions.AuthorizeFolder("/");
                    options.Conventions.AllowAnonymousToPage("/Login");
                    options.Conventions.ConfigureFilter(new AutoValidateAntiforgeryTokenAttribute());
                });


            builder.Services.AddHttpContextAccessor();

            builder.Services
                .AddControllers()
                .AddApplicationPart(typeof(UsersController).Assembly);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ITaskCommentService, TaskCommentService>();
            builder.Services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
            builder.Services.AddScoped<ITaskItemService, TaskItemService>();
            builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjecRepository, ProjecRepository>();
            builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();
            builder.Services.AddScoped<IJwtHelper, JwtHelper>();
            builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<ILoginValidator, LoginValidator>();

            //Database
            //builder.Services.AddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));
            //builder.Services.AddScoped<IProjectOperationsRepository, ProjectOperationsRepository>();
            //builder.Services.AddScoped<IUserOperationsRepository, UserOperationsRepository>();
            //builder.Services.AddScoped<ITaskItemOperationsRepository, TaskItemOperationsRepository>();
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //   options.UseSqlite($"Data Source={dbPath}"));

            //In Memory
            builder.Services.AddSingleton<InMemoryDbContext>();
            builder.Services.AddScoped(typeof(IDbRepository<>), typeof(InMemoryRepository<>));
            builder.Services.AddScoped<IProjectOperationsRepository, InMemoryProjectOperationsRepository>();
            builder.Services.AddScoped<ITaskItemOperationsRepository, InMemoryTaskItemOperationsRepository>();
            builder.Services.AddScoped<IUserOperationsRepository, InMemoryUserOperationsRepository>();
        }
    }
}