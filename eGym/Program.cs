using eGym;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
            
        using(var scope= host.Services.CreateScope())
        {
            var services= scope.ServiceProvider;
            try
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                eGym.Models.AppDbContextSeed.SeedRoleAsync(roleManager).Wait();
            }
            catch (Exception)
            {
                throw;
            }
        }
            
            host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
