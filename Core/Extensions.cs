namespace Landlords.Core
{
    using System;
    using System.Security.Claims;
    using Database;
    using DataProviders;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.DependencyInjection;
    using Repositories;
    using System.Linq;
    using Model.Database;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Jwt;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Services;
    using Landlords.Permissions;
    using Microsoft.AspNetCore.Authorization;
    using Model;

    public static class Extensions
    {
        public static IEnumerable<IdentityError> ToGeneric(this IEnumerable<IdentityError> errors)
        {
            return errors.Select(c => new IdentityError {Code = "GenericError", Description = c.Description});
        }

        public static object ToErrorCollection(this IEnumerable<IdentityError> errors)
        {
            return errors.Select(c => new { Key = c.Code, Value = new[] {c.Description} })
                .ToList();
        }

        public static object ToErrorCollection(this ModelStateDictionary modelState)
        {
            return modelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                .Select(c => new {c.Key, c.Value})
                .ToList();
        }

        public static bool IsDefault(this Guid guid)
        {
            return guid == default(Guid);
        }

        public static bool IsValid(this ICollection<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return false;
            }

            var result = true;

            foreach (var formFile in files)
            {
                if (formFile.Length > Constants.UPLOAD_LIMIT)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static string AsGenericError(this string message)
        {
            return JsonConvert.SerializeObject(new
            {
                errors = new[]
                {
                    new {key="GenericError", value=new [] { message }}
                }
            });
        }

        public static Guid GetAgencyId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetIdByClaimType(LLClaimTypes.AgencyIdentifier);
        }

        public static Guid GetPortfolioId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetIdByClaimType(LLClaimTypes.PortfolioIdentifier);
        }

        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetIdByClaimType(ClaimTypes.NameIdentifier);
        }

        public static Guid GetIdByClaimType(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            if (claimsPrincipal == null) throw new ArgumentNullException(nameof(claimsPrincipal));

            var identifier = claimsPrincipal.FindFirst(claimType)?.Value;
            if (identifier != null)
            {
                var success = Guid.TryParse(identifier, out Guid result);
                if (success)
                {
                    return result;
                }
            }

            throw new InvalidOperationException("Unable to determine claim");
        }

        public static ApplicationUser GetApplicationUser(this ClaimsPrincipal claimsPrincipal, LLDbContext context)
        {
            var userId = claimsPrincipal.GetUserId();
            if (userId != default(Guid))
            {
                return context.Users.FirstOrDefault(c => c.Id == userId);
            }

            throw new InvalidOperationException("Unable to determine User");
        }

        public static IServiceCollection RegisterDI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPropertyDataProvider, PropertyDataProvider>()
                .AddScoped<IPropertyImageDataProvider, PropertyImageDataProvider>()
                .AddScoped<ILandlordDataProvider, LandlordDataProvider>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ILLDbContext, LLDbContext>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>()
                .AddTransient<IEmailSender, EmailSender>();

            return serviceCollection;
        }
    }
}