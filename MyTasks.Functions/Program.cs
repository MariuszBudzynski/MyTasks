using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.DbOperations.Repositories;
using MyTasks.Repositories.Interfaces.IProjecRepository;
using MyTasks.Repositories.Repositories.ProjecRepository;

var builder = FunctionsApplication.CreateBuilder(args);
var relativePath = builder.Configuration.GetConnectionString("DefaultConnection");
var projectFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
var dbPath = Path.Combine(projectFolder, relativePath);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights()
    .AddDbContext<AppDbContext>(options =>
     options.UseSqlite($"Data Source={dbPath}"))

    .AddScoped<IProjecRepository, ProjecRepository>()
    .AddScoped<IProjectOperationsRepository, ProjectOperationsRepository>()
    .AddScoped(typeof(IDbRepository<>), typeof(DbRepository<>));

await builder.Build().RunAsync();