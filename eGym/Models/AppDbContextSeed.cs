using Microsoft.AspNetCore.Identity;

namespace eGym.Models
{
    public static class AppDbContextSeed
    {
       public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }
    }
}
