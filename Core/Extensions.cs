namespace Landlords.Core
{
    using System;
    using System.Security.Claims;
    using Microsoft.Extensions.DependencyInjection;
    using Landlords.DataProviders;
    using Landlords.Repositories;
    using Landlords.Database;

    public static class Extensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null) throw new ArgumentNullException(nameof(claimsPrincipal));

            var nameIdentifier = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (nameIdentifier != null)
            {
                var success = Guid.TryParse(nameIdentifier, out Guid result);
                if (success)
                {
                    return result;
                }
            }

            throw new InvalidOperationException("Unable to determine User");
        }

        public static void RegisterDI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPropertyDataProvider, PropertyDataProvider>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ILLDbContext, LLDbContext>();
        }
    }
}