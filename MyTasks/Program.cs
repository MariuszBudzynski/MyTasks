using MyTasks.Repositories.Interfaces.IUserDataRepository;
using MyTasks.Services;

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

// Minimal API Example with DI
app.MapGet("/api/allUsers", async (IUserDataRepository repo) =>
{
    var userData = await repo.GetAllUserData();

    if (!userData.Any())
        return Results.NotFound("No users found.");

    var results = userData.Select( u => new
    {
        u.Id,
        u.LoginModel?.Username,
        u.FullName,
        u.IsDeleted,
    });

    return Results.Ok(results);
});

await app.RunAsync();
