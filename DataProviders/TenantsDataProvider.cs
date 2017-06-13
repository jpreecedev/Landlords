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

        public async Task<ICollection<TenantViewModel>> GetTenantsAsync(Guid portfolioId)
        {
            return await (from tenant in Context.Tenants.AsNoTracking()
                    join tenantAddress in Context.TenantAddresses on tenant.Id equals tenantAddress.TenantId into tenantAddressJoin
                    where tenant.PortfolioId == portfolioId && !tenant.IsDeleted && (tenant.FirstName != null || tenant.LastName != null)
                    select new TenantViewModel(tenant, tenantAddressJoin.Where(c => !c.IsDeleted).ToList())
                )
                .ToListAsync();
        }

        public async Task<ICollection<TenantViewModel>> GetTenantsForPropertyAsync(Guid portfolioId, Guid propertyDetailsId)
        {
            return await (from tenant in Context.Tenants.AsNoTracking()
                    join propertyDetails in Context.PropertyDetails on propertyDetailsId equals propertyDetailsId
                    join tenantAddress in Context.TenantAddresses on tenant.Id equals tenantAddress.TenantId into tenantAddressJoin
                    where tenant.PortfolioId == portfolioId && !tenant.IsDeleted && (tenant.FirstName != null || tenant.LastName != null)
                    select new TenantViewModel(tenant, tenantAddressJoin.Where(c => !c.IsDeleted).ToList())
                )
                .ToListAsync();
        }

        public async Task<TenantViewModel> NewAsync(Guid portfolioId)
        {
            var entity = new Tenant
            {
                Created = DateTime.Now,
                PortfolioId = portfolioId
            };
            
            await Context.Tenants.AddAsync(entity);

            var addressEntity = new TenantAddress
            {
                TenantId = entity.Id
            };

            await Context.TenantAddresses.AddAsync(addressEntity);
            await Context.SaveChangesAsync();

            return new TenantViewModel(entity, new List<TenantAddress>());
        }

        public async Task<TenantViewModel> GetTenantByIdAsync(Guid portfolioId, Guid tenantId)
        {
            return await (from tenant in Context.Tenants.AsNoTracking()
                          join tenantAddresses in Context.TenantAddresses on tenant.Id equals tenantAddresses.TenantId into tenantAddressesJoin
                          where tenant.PortfolioId == portfolioId && tenant.Id == tenantId && !tenant.IsDeleted
                          select new TenantViewModel(tenant, tenantAddressesJoin.Where(c => !c.IsDeleted).ToList())
                          )
                          .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Guid portfolioId, TenantViewModel tenant)
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