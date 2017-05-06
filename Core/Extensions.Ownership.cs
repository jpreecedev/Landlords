namespace Landlords.Core
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.EntityFrameworkCore;

    public static class Ownership
    {
        public static async Task<bool> OwnsPropertyImageAsync(this Guid userId, Guid propertyImageId, ILLDbContext context)
        {
            if (userId.IsDefault() || propertyImageId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.ApplicationUserPortfolios.Include(x => x.Portfolio)
                .Where(aup => aup.Portfolio.HasUser(userId))
                .Join(context.PropertyDetails, aup => aup.PortfolioId, details => details.PortfolioId, (portfolio, details) => new {Portfolio = portfolio, PropertyDetails = details})
                .Join(context.PropertyImages, arg => arg.PropertyDetails.Id, image => image.PropertyId, (details, image) => new {PropertyDetails = details, PropertyImage = image})
                .AnyAsync(c => c.PropertyImage.Id == propertyImageId);
        }

        public static async Task<bool> OwnsPropertyDetailsAsync(this Guid userId, Guid propertyId, ILLDbContext context)
        {
            if (userId.IsDefault() || propertyId.IsDefault() || context == null)
            {
                return false;
            }

            return await context.PropertyDetails.Include(x => x.Portfolio).AnyAsync(c => c.Portfolio.HasUser(userId) && c.Id == propertyId);
        }
    }
}