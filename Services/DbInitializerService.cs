using Landlords.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Landlords.Services
{
    using Core;

    public class DbInitializerService : IDbInitializerService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<LLDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (LLDbContext context = serviceScope.ServiceProvider.GetService<LLDbContext>())
                {
                    var roleManager = serviceScope.ServiceProvider.GetService<ApplicationRoleManager>();
                    var userManager = serviceScope.ServiceProvider.GetService<ApplicationUserManager>();

                    context.SeedDatabase(roleManager, userManager);
                }
            }
        }
    }
}
