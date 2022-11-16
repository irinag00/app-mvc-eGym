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

        public static async Task SeedAdminAsync(UserManager<IdentityUser> userManager)
        {
            var userAdmin= userManager.Users.Where(x=>x.Email== UserRoles.MailAdmin).FirstOrDefault();
            if (userAdmin != null) return;

            userAdmin = new IdentityUser
            {
                UserName = UserRoles.MailAdmin,
                Email = UserRoles.MailAdmin,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            await userManager.CreateAsync(userAdmin, "admirina");
            await userManager.AddToRoleAsync(userAdmin, UserRoles.Admin);
        }
    }
}
