using FIAP.Cloud.Games.Identity.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FIAP.Cloud.Games.Identity.Data.Seeds
{
    public static class UserSeed
    {
        public static async Task SeedAdminUserAsync(UserManager<CurrentUser> userManager, IConfigurationRoot configuration)
        {
            var adminUser = await userManager.FindByEmailAsync(configuration["ADMIN_EMAIL"]);

            if (adminUser == null)
            {
                adminUser = new CurrentUser
                {
                    UserName = configuration["ADMIN_EMAIL"],
                    Email = configuration["ADMIN_EMAIL"],
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, configuration["ADMIN_PASSWORD"]);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    Console.WriteLine("Falha ao criar usuário admin:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"- {error.Description}");
                    }
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("Usuário admin já existia, mas foi adicionado à role 'Admin'.");
                }
                else
                {
                    Console.WriteLine("Usuário admin e role 'Admin' já existem.");
                }
            }
        }

        public static async Task UserSeedAsync(IServiceProvider serviceProvider, IConfigurationRoot configuration)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CurrentUser>>();

            try
            {
                await SeedAdminUserAsync(userManager, configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao seedar usuário admin", ex);
            }
        }
    }
}