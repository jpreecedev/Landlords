namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Model.Entities;
    using ViewModels;

    public class TenantsDataProvider : BaseDataProvider, ITenantsDataProvider
    {
        public TenantsDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ICollection<TenantViewModel>> GetTenantsByPortfolioIdAsync(Guid portfolioId)
        {
            //Untested

            return await (from tenant in Context.Tenants.AsNoTracking()
                          join tt in Context.TenantTenancies.Include(x => x.Tenancy.PropertyDetails) on tenant.Id equals tt.TenantId
                          join tenantAddress in Context.TenantAddresses on tenant.Id equals tenantAddress.TenantId into tenantAddressJoin
                          join tenantContacts in Context.TenantContacts on tenant.Id equals tenantContacts.TenantId into tenantContactsJoin
                          where tt.Tenancy.PropertyDetails.PortfolioId == portfolioId && tenant.IsLeadTenant && !tenant.IsDeleted && (tenant.FirstName != null || tenant.LastName != null)
                          select new TenantViewModel(tenant, tenantAddressJoin.Where(c => !c.IsDeleted).ToList(), tenantContactsJoin.Where(c => !c.IsDeleted).ToList())
                )
                .ToListAsync();
        }

        public async Task<ICollection<TenantViewModel>> GetTenantsForPropertyAsync(Guid portfolioId, Guid propertyDetailsId)
        {
            //Untested

            return await (from tenant in Context.Tenants.AsNoTracking()
                          join tt in Context.TenantTenancies.Include(x => x.Tenancy.PropertyDetails) on tenant.Id equals tt.TenantId
                          join tenantAddress in Context.TenantAddresses on tenant.Id equals tenantAddress.TenantId into tenantAddressJoin
                          join tenantContacts in Context.TenantContacts on tenant.Id equals tenantContacts.TenantId into tenantContactsJoin
                          where tt.Tenancy.PropertyDetails.PortfolioId == portfolioId && tt.Tenancy.PropertyDetailsId == propertyDetailsId && tenant.IsLeadTenant && !tenant.IsDeleted && (tenant.FirstName != null || tenant.LastName != null)
                          select new TenantViewModel(tenant, tenantAddressJoin.Where(c => !c.IsDeleted).ToList(), tenantContactsJoin.Where(c => !c.IsDeleted).ToList())
                )
                .ToListAsync();
        }

        public async Task<ICollection<TenantViewModel>> GetTenantsByAgencyIdAsync(Guid agencyId)
        {
            //Untested

            return await (from tenant in Context.Tenants.AsNoTracking()
                join tenantAddress in Context.TenantAddresses on tenant.Id equals tenantAddress.TenantId into tenantAddressJoin
                join tenantContacts in Context.TenantContacts on tenant.Id equals tenantContacts.TenantId into tenantContactsJoin
                join tenantTenancies in Context.TenantTenancies on tenant.Id equals tenantTenancies.TenantId into tenantTenanciesJoin
                from tt in tenantTenanciesJoin
                join tenancies in Context.Tenancies on tt.TenancyId equals tenancies.Id
                join propertyDetails in Context.PropertyDetails on tenancies.PropertyDetailsId equals propertyDetails.Id
                join aup in Context.ApplicationUserPortfolios on propertyDetails.PortfolioId equals aup.PortfolioId
                where aup.AgencyId == agencyId && !tenancies.IsDeleted && !propertyDetails.IsDeleted && !aup.IsDeleted && tenant.IsLeadTenant && !tenant.IsDeleted && (tenant.FirstName != null || tenant.LastName != null)
                select new TenantViewModel(tenant, tenantAddressJoin.Where(c => !c.IsDeleted).ToList(), tenantContactsJoin.Where(c => !c.IsDeleted).ToList())).ToListAsync();
        }

        public async Task<TenantViewModel> NewAsync()
        {
            var entity = new Tenant
            {
                Created = DateTime.Now,
            };

            await Context.Tenants.AddAsync(entity);

            var addressEntity = new TenantAddress
            {
                TenantId = entity.Id,
                Created = DateTime.Now
            };

            await Context.TenantAddresses.AddAsync(addressEntity);
            await Context.SaveChangesAsync();

            return new TenantViewModel(entity, new List<TenantAddress>(), new List<TenantContact>());
        }

        public async Task<TenantViewModel> GetTenantByIdAsync(Guid tenantId)
        {
            return await (from tenant in Context.Tenants.AsNoTracking()
                          join tenantAddresses in Context.TenantAddresses on tenant.Id equals tenantAddresses.TenantId into tenantAddressesJoin
                          join tenantContacts in Context.TenantContacts on tenant.Id equals tenantContacts.TenantId into tenantContactsJoin
                          where tenant.Id == tenantId && !tenant.IsDeleted
                          select new TenantViewModel(tenant, tenantAddressesJoin.Where(c => !c.IsDeleted).ToList(), tenantContactsJoin.Where(c => !c.IsDeleted).ToList())
                          )
                          .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TenantViewModel tenant)
        {
            var existingEntity = await Context.Tenants.Include(x => x.Addresses).FirstOrDefaultAsync(c => c.Id == tenant.Id && !c.IsDeleted);
            if (existingEntity != null)
            {
                existingEntity.MapFrom(tenant);

                if (tenant.Addresses != null && tenant.Addresses.Any())
                {
                    foreach (var existingEntityAddress in existingEntity.Addresses)
                    {
                        var updatedEntity = tenant.Addresses.FirstOrDefault(c => c.Id == existingEntityAddress.Id);
                        if (updatedEntity != null)
                        {
                            existingEntityAddress.MapFrom(updatedEntity);
                        }
                    }
                }

                Context.Tenants.Update(existingEntity);
                await Context.SaveChangesAsync();
            }
        }
    }
}