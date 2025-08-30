using MyTasks.Repositories.Interfaces.IUserDataRepository;
using MyTasks.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

ServisRegistration.Register(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//JWT config
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Login");
        return;
    }
    await next();
});

app.MapRazorPages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

//Logout
app.MapPost("/logout", (HttpResponse response) =>
{
    response.Cookies.Delete("AuthToken");
    response.Redirect("/Login");
    return Results.Empty;
});

// Minimal API Example with DI
app.MapGet("/minimal-api/users", async (IUserDataRepository repo) =>
{
    var users = await repo.GetAllUserData();

    if (!users.Any())
        return Results.NotFound("No users found.");

    var results = users.Select(u => new
    {
        u.Id,
        u.Username,
        u.FullName,
        u.Type,
        u.IsDeleted
    });

    return Results.Ok(results);
});

await app.RunAsync();