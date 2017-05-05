namespace Landlords.Core
{
    using System;
    using Database;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using Model;
    using Model.Database;

    public static class SeedExtensions
    {
        public static void Seed(this LLDbContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<ApplicationRoleManager>();
            var userManager = serviceProvider.GetService<ApplicationUserManager>();

            var tasks = new List<Task>
            {
                CreateRoles(context, roleManager),
                CreateUsers(context, userManager)
            };
            Task.WaitAll(tasks.ToArray());
        }

        private static async Task CreateRoles(LLDbContext context, ApplicationRoleManager roleManager)
        {
            if (!await context.Roles.AnyAsync())
            {
                foreach (var role in ApplicationRoles.AllRoles)
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }
        }

        private static async Task CreateUsers(LLDbContext context, ApplicationUserManager userManager)
        {
            if (await userManager.Users.AnyAsync(c => c.UserName == "admin@landlords.com"))
            {
                return;
            }

            var administrator = new ApplicationUser
            {
                UserName = "admin@landlords.com",
                Email = "admin@landlords.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Admin"
            };

            await userManager.CreateAsync(administrator, "Password123");
            await userManager.AddToRoleAsync(administrator, ApplicationRoles.SiteAdministrator);
            await userManager.SetUserPermissionsAsync(administrator.Id, Permissions.ProfileView, Permissions.ProfileUpdate);
        }
    }
}