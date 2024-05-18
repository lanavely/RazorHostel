using Auto.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auto.Roles;

public static class RoleInitializer
{
    public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync(Consts.Admin) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Consts.Admin));
        }
        
        if (await roleManager.FindByNameAsync(Consts.Instructor) == null)
        {
                await roleManager.CreateAsync(new IdentityRole(Consts.Instructor));
        }

        if (await roleManager.FindByNameAsync(Consts.Student) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Consts.Student));
        }

        var adminEmail = "admin@gmail.com";
        var adminName = "admin";
        if (await userManager.FindByNameAsync(adminEmail) == null)
        {
            var admin = new AppUser { Email = adminEmail, UserName = adminName, PhoneNumber = "+79032432523", FirstName = adminName, LastName = adminName};
            var result = await userManager.CreateAsync(admin, "123456");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}