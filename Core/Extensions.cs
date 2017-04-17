﻿namespace Landlords.Core
{
    using System;
    using System.Collections;
    using System.Security.Claims;
    using Database;
    using DataProviders;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.DependencyInjection;
    using Repositories;
    using System.Linq;

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
                    return result;
            }

            throw new InvalidOperationException("Unable to determine User");
        }

        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return modelState.ToDictionary(kvp => kvp.Key,
                        kvp => kvp.Value.Errors
                            .Select(e => e.ErrorMessage)
                            .ToArray())
                    .Where(m => m.Value.Count() > 0);
            return null;
        }

        public static void RegisterDI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPropertyDataProvider, PropertyDataProvider>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ILLDbContext, LLDbContext>();
        }
    }
}