using Auto.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auto.Roles;

public static class RoleInitializer
{
    public static async Task InitializeAsync(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync("admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
        }
        
        if (await roleManager.FindByNameAsync("instructor") == null)
        {
                await roleManager.CreateAsync(new IdentityRole("instructor"));
        }

        if (await roleManager.FindByNameAsync("student") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("student"));
        }

        var adminEmail = "admin@gmail.com";
        if (await userManager.FindByNameAsync(adminEmail) == null)
        {
            var admin = new UserEntity { Email = adminEmail, UserName = adminEmail, PhoneNumber = "+79032432523"};
            var result = await userManager.CreateAsync(admin, "123456");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}