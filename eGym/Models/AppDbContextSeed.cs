using Microsoft.AspNetCore.Identity;

namespace eGym.Models
{
    public static class AppDbContextSeed
    {
       public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            await roleManager.CreateAsync(new IdentityRole(Roles.User));
        }

        public static async Task SeedAdminAsync(UserManager<IdentityUser> userManager)
        {
            var userAdmin= userManager.Users.Where(x=>x.Email== Roles.MailAdmin).FirstOrDefault();
            if (userAdmin != null) return;

            userAdmin = new IdentityUser
            {
                UserName = Roles.MailAdmin,
                Email = Roles.MailAdmin,
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            await userManager.CreateAsync(userAdmin, "ex8eS%08MQ7I");
            await userManager.AddToRoleAsync(userAdmin, Roles.Admin);
        }
    }
}
