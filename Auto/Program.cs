using Auto.Data;
using Auto.Data.Entities;
using Auto.Importer;
using Auto.Mapping;
using Auto.Roles;
using Auto.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("HostelDb") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var services = builder.Services;
services.AddDbContext<ApplicationDbContext>(contextBuilder =>
{
    contextBuilder.UseNpgsql(connectionString);
});

services.AddDatabaseDeveloperPageExceptionFilter();

services
    .AddDefaultIdentity<AppUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
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
services.AddRazorPages();
services.AddScoped<TestService>();
services.AddAutoMapper(typeof(AppMappingProfile));

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
var serviceProvider = scope.ServiceProvider;

if (false)
{
    var importer = new Importer(serviceProvider.GetRequiredService<ApplicationDbContext>());
    await importer.ImportAsync();
    return;
}

var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
var rolesManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

await RoleInitializer.InitializeAsync(userManager, rolesManager);

app.Run();
