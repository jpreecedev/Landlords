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
    using System.ComponentModel.DataAnnotations;
    using Jwt;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Services;
    using Landlords.Permissions;
    using Microsoft.AspNetCore.Authorization;
    using Landlords.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Model.DataTypes;
    using Model.Entities;
    using Statements;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Serialization;

    public static class Extensions
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string GetAccessToken(this HttpRequest request)
        {
            return request.Query["access_token"].FirstOrDefault();
        }

        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            return DateTime.Parse($"1/{dateTime.Month}/{dateTime.Year}");
        }

        public static ICollection<T> AddRange<T>(this ICollection<T> collection, ICollection<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        public static decimal ToDecimal(this string input)
        {
            if (decimal.TryParse(input, out decimal result))
            {
                return result;
            }

            return 0;
        }

        public static IStatementProcessor GetStatementProcessor(this Account account)
        {
            switch (account.ProviderName)
            {
                case AccountProviders.LloydsBank:
                    return new LloydsCSV();

                case AccountProviders.Santander:
                    return new SantanderCSV();

                default:
                    throw new ArgumentOutOfRangeException("No statement processor available");
            }
        }

        public static IEnumerable<IdentityError> ToGeneric(this IEnumerable<IdentityError> errors)
        {
            return errors.Select(c => new IdentityError { Code = "GenericError", Description = c.Description });
        }

        public static object ToErrorCollection(this IEnumerable<IdentityError> errors)
        {
            return errors.Select(c => new { Key = c.Code, Value = new[] { c.Description } })
                .ToList();
        }

        public static object ToErrorCollection(this IEnumerable<ValidationResult> modelState)
        {
            var modelErrors = new ModelStateDictionary();
            foreach (var result in modelState)
            {
                foreach (var member in result.MemberNames)
                {
                    modelErrors.AddModelError(member, result.ErrorMessage);
                }
            }
            return modelErrors.ToErrorCollection();
        }

        public static object ToErrorCollection(this ModelStateDictionary modelState)
        {
            return modelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                .Select(c => new { c.Key, c.Value })
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

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, _jsonSerializerSettings);
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

        public static async Task<ApplicationUser> GetApplicationUserAsync(this ClaimsPrincipal claimsPrincipal, ILLDbContext context)
        {
            var userId = claimsPrincipal.GetUserId();
            if (userId != default(Guid))
            {
                return await context.Users.FirstOrDefaultAsync(c => c.Id == userId);
            }

            throw new InvalidOperationException("Unable to determine User");
        }

        public static async Task<ApplicationUser> GetSiteAdministratorAsync(this DbSet<ApplicationUser> applicationUsers)
        {
            return await applicationUsers.SingleAsync(c => c.Email == "admin@landlords.com");
        }

        public static bool IsSiteAdministrator(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole(ApplicationRoles.SiteAdministrator);
        }

        public static bool IsAgencyUser(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole(ApplicationRoles.AgencyAdministrator) || claimsPrincipal.IsInRole(ApplicationRoles.AgencyUser);
        }

        public static bool IsTenant(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole(ApplicationRoles.Tenant);
        }

        public static bool IsLandlord(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole(ApplicationRoles.Landlord);
        }
       
        public static bool HasPropertyPortfolio(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(LLClaimTypes.PortfolioIdentifier) != null;
        }

        public static IServiceCollection RegisterDI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPropertyDataProvider, PropertyDataProvider>()
                .AddScoped<IPropertyImageDataProvider, PropertyImageDataProvider>()
                .AddScoped<ILandlordDataProvider, LandlordDataProvider>()
                .AddScoped<IPermissionsDataProvider, PermissionsDataProvider>()
                .AddScoped<IChecklistDataProvider, ChecklistDataProvider>()
                .AddScoped<IChecklistInstanceDataProvider, ChecklistInstanceDataProvider>()
                .AddScoped<IChecklistItemDataProvider, ChecklistItemDataProvider>()
                .AddScoped<IFinancesDataProvider, FinancesDataProvider>()
                .AddScoped<INotificationsDataProvider, NotificationsDataProvider>()
                .AddScoped<ITransactionsDataProvider, TransactionsDataProvider>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ILLDbContext, LLDbContext>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>()
                .AddScoped<ITenantsDataProvider, TenantsDataProvider>()
                .AddScoped<ITenanciesDataProvider, TenanciesDataProvider>()
                .AddScoped<IJourneyDataProvider, JourneyDataProvider>()
                .AddScoped<IShortlistedPropertiesDataProvider, ShortlistedPropertiesDataProvider>()
                .AddScoped<IConversationDataProvider, ConversationDataProvider>()
                .AddScoped<IMaintenanceRequestsDataProvider, MaintenanceRequestsDataProvider>()
                .AddTransient<IEmailSender, EmailSender>();

            return serviceCollection;
        }
    }
}