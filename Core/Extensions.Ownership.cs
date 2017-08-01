namespace Landlords.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.EntityFrameworkCore;
    using Model.Entities;

    public static class Ownership
    {
        public static async Task<bool> OwnsPropertyImageAsync(this Guid userId, Guid propertyImageId, ILLDbContext context)
        {
            if (userId.IsDefault() || propertyImageId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ApplicationUserPortfolios.Include(x => x.Portfolio.Users)
                .Where(aup => aup.Portfolio.Users.Any(link => link.UserId == userId))
                .Join(context.PropertyDetails, aup => aup.PortfolioId, details => details.PortfolioId, (portfolio, details) => new { Portfolio = portfolio, PropertyDetails = details })
                .Join(context.PropertyImages, arg => arg.PropertyDetails.Id, image => image.PropertyId, (details, image) => new { PropertyDetails = details, PropertyImage = image })
                .AnyAsync(c => c.PropertyImage.Id == propertyImageId);
        }

        public static async Task<bool> OwnsPropertyDetailsAsync(this Guid userId, Guid propertyId, ILLDbContext context)
        {
            if (userId.IsDefault() || propertyId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.PropertyDetails.Include(x => x.Portfolio.Users).AnyAsync(c => c.Portfolio.Users.Any(link => link.UserId == userId) && c.Id == propertyId);
        }

        public static async Task<bool> OwnsPortfolioAsync(this Guid userId, Guid portfolioId, ILLDbContext context)
        {
            if (userId.IsDefault() || portfolioId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ApplicationUserPortfolios.AnyAsync(c => c.UserId == userId && c.PortfolioId == portfolioId);
        }

        public static async Task<bool> OwnsAccountAsync(this Guid userId, Guid portfolioId, Guid accountId, ILLDbContext context)
        {
            if (userId.IsDefault() || portfolioId.IsDefault() || accountId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ApplicationUserPortfolios.AsNoTracking()
                .Join(context.Accounts, aup => aup.PortfolioId, account => account.PortfolioId, (portfolio, account) => new { ApplicationUserPortfolio = portfolio, Account = account })
                .Where(c => c.ApplicationUserPortfolio.UserId == userId && c.ApplicationUserPortfolio.PortfolioId == portfolioId && c.Account.Id == accountId)
                .AnyAsync();
        }

        public static async Task<bool> HasARelationshipAsync(this Guid tenantId, Guid landlordId, ILLDbContext context)
        {
            if (tenantId.IsDefault() || landlordId.IsDefault() || context == null)
            {
                return false;
            }

            return await (from applicationUser in context.Users.AsNoTracking()
                    join aup in context.ApplicationUserPortfolios on applicationUser.Id equals aup.UserId
                    join portfolio in context.Portfolios on aup.PortfolioId equals portfolio.Id
                    join propertyDetails in context.PropertyDetails on portfolio.Id equals propertyDetails.PortfolioId
                    join tenancies in context.Tenancies on propertyDetails.Id equals tenancies.PropertyDetailsId into tenanciesJoin
                    from tenancy in tenanciesJoin
                    join tenantTenancy in context.TenantTenancies on tenancy.Id equals tenantTenancy.TenancyId
                    join tenant in context.Tenants on tenantTenancy.TenantId equals tenant.Id
                    where applicationUser.Id == landlordId
                    select applicationUser)
                .AnyAsync();
        }
    }
}