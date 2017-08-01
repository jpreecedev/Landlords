namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using System.Linq;

    public class TenanciesDataProvider : BaseDataProvider, ITenanciesDataProvider
    {
        public TenanciesDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ICollection<TenantTenancyViewModel>> GetTenanciesByPortfolioIdAsync(Guid portfolioId)
        {
            //untested
            return await (from portfolio in Context.Portfolios.AsNoTracking()
                          join propertyDetails in Context.PropertyDetails on portfolio.Id equals propertyDetails.PortfolioId into propertyDetailsJoin
                          from property in propertyDetailsJoin
                          join tenancy in Context.Tenancies on property.Id equals tenancy.PropertyDetailsId into tenanciesJoin
                          from tenancyItem in tenanciesJoin
                          join tenantTenancy in Context.TenantTenancies on tenancyItem.Id equals tenantTenancy.TenancyId into tenantTenanciesJoin
                          from tenantTenancyItem in tenantTenanciesJoin
                          join tenant in Context.Tenants on tenantTenancyItem.TenantId equals tenant.Id
                          join applicationUser in Context.Users on tenant.ApplicationUserId equals applicationUser.Id
                          where portfolio.Id == portfolioId && !property.IsDeleted && !tenancyItem.IsDeleted && !tenantTenancyItem.IsDeleted && !tenant.IsDeleted && tenant.IsLeadTenant
                    select new TenantTenancyViewModel
                    {
                        TenancyId = tenancyItem.Id,
                        LeadTenant = applicationUser.Name,
                        TenantPhoneNumber = applicationUser.PhoneNumber,
                        TenantSecondaryPhoneNumber = applicationUser.SecondaryPhoneNumber,
                        TenantEmailAddress = applicationUser.Email,
                        PropertyReference = property.Reference,
                        TenancyEndDate = tenancyItem.EndDate
                    })
                .ToListAsync();
        }

        public async Task<ICollection<TenantTenancyViewModel>> GetTenanciesByAgencyIdAsync(Guid agencyId)
        {
            return null;
        }

        public async Task<TenantTenancyViewModel> GetTenancyByIdAsync(Guid portfolioId, Guid tenancyId)
        {
            return null;
        }
    }
}