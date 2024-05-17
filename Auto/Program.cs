using Auto.Data;
using Auto.Data.Entities;
using Auto.Importer;
using Auto.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HostelDb") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(contextBuilder =>
{
    contextBuilder.UseNpgsql(connectionString);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<AppUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password = new PasswordOptions()
        {
            RequiredLength = 3,
            RequireDigit = true,
            RequireLowercase = false,
            RequireUppercase = false,
            RequiredUniqueChars = 3,
            RequireNonAlphanumeric = false
        };
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();

if (false)
{
    var importer = new Importer(services.GetRequiredService<ApplicationDbContext>());
    await importer.ImportAsync();
    return;
}

await RoleInitializer.InitializeAsync(userManager, rolesManager);

app.Run();
