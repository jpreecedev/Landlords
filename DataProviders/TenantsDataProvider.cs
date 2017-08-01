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
                    join applicationUser in Context.Users on tenant.ApplicationUserId equals applicationUser.Id
                    join tenantAddress in Context.TenantAddresses on tenant.Id equals tenantAddress.TenantId into tenantAddressJoin
                    join tenantContacts in Context.TenantContacts on tenant.Id equals tenantContacts.TenantId into tenantContactsJoin
                    join tt in Context.TenantTenancies.Include(x => x.Tenancy.PropertyDetails) on tenant.Id equals tt.TenantId
                    where tt.Tenancy.PropertyDetails.PortfolioId == portfolioId && tenant.IsLeadTenant && !tenant.IsDeleted
                    select new TenantViewModel
                    {
                        Id = tenant.Id,
                        FirstName = applicationUser.FirstName,
                        LastName = applicationUser.LastName,
                        IsLeadTenant = tenant.IsLeadTenant,
                        MainContactNumber = applicationUser.PhoneNumber,
                        EmailAddress = applicationUser.Email,
                        SecondaryContactNumber = applicationUser.SecondaryPhoneNumber,
                        Addresses = tenantAddressJoin.Where(c => !c.IsDeleted).Select(c => new TenantAddressViewModel(!tenant.IsAdult, c)).ToList(),
                        Title = applicationUser.Title,
                        CompanyName = tenant.CompanyName,
                        IsAdult = tenant.IsAdult,
                        DateOfBirth = tenant.DateOfBirth,
                        MiddleName = applicationUser.MiddleName,
                        IsSmoker = tenant.IsSmoker,
                        HasPets = tenant.HasPets,
                        WorkContactNumber = tenant.WorkContactNumber,
                        PassportReference = tenant.PassportReference,
                        Occupation = tenant.Occupation,
                        WorkAddress = tenant.WorkAddress,
                        AdditionalInformation = tenant.AdditionalInformation,
                        DrivingLicenseReference = tenant.DrivingLicenseReference,
                        Contacts = tenantContactsJoin.Where(c => !c.IsDeleted).Select(c => new TenantContactViewModel(c)).ToList()
                    }
                )
                .ToListAsync();
        }

        public async Task<ICollection<TenantViewModel>> GetTenantsForPropertyAsync(Guid portfolioId, Guid propertyDetailsId)
        {
            //Untested
            return await (from tenant in Context.Tenants.AsNoTracking()
                    join applicationUser in Context.Users on tenant.ApplicationUserId equals applicationUser.Id
                    join tt in Context.TenantTenancies.Include(x => x.Tenancy.PropertyDetails) on tenant.Id equals tt.TenantId
                    join tenantAddress in Context.TenantAddresses on tenant.Id equals tenantAddress.TenantId into tenantAddressJoin
                    join tenantContacts in Context.TenantContacts on tenant.Id equals tenantContacts.TenantId into tenantContactsJoin
                    where tt.Tenancy.PropertyDetails.PortfolioId == portfolioId && tt.Tenancy.PropertyDetailsId == propertyDetailsId && tenant.IsLeadTenant && !tenant.IsDeleted && (applicationUser.FirstName != null || applicationUser.LastName != null)
                    select new TenantViewModel
                    {
                        Id = tenant.Id,
                        FirstName = applicationUser.FirstName,
                        LastName = applicationUser.LastName,
                        IsLeadTenant = tenant.IsLeadTenant,
                        MainContactNumber = applicationUser.PhoneNumber,
                        EmailAddress = applicationUser.Email,
                        SecondaryContactNumber = applicationUser.SecondaryPhoneNumber,
                        Addresses = tenantAddressJoin.Where(c => !c.IsDeleted).Select(c => new TenantAddressViewModel(!tenant.IsAdult, c)).ToList(),
                        Title = applicationUser.Title,
                        CompanyName = tenant.CompanyName,
                        IsAdult = tenant.IsAdult,
                        DateOfBirth = tenant.DateOfBirth,
                        MiddleName = applicationUser.MiddleName,
                        IsSmoker = tenant.IsSmoker,
                        HasPets = tenant.HasPets,
                        WorkContactNumber = tenant.WorkContactNumber,
                        PassportReference = tenant.PassportReference,
                        Occupation = tenant.Occupation,
                        WorkAddress = tenant.WorkAddress,
                        AdditionalInformation = tenant.AdditionalInformation,
                        DrivingLicenseReference = tenant.DrivingLicenseReference,
                        Contacts = tenantContactsJoin.Where(c => !c.IsDeleted).Select(c => new TenantContactViewModel(c)).ToList()
                    }
                )
                .ToListAsync();
        }

        public async Task<ICollection<TenantViewModel>> GetTenantsByAgencyIdAsync(Guid agencyId)
        {
            //Untested

            return await (from tenant in Context.Tenants.AsNoTracking()
                    join applicationUser in Context.Users on tenant.ApplicationUserId equals applicationUser.Id
                    join tenantAddress in Context.TenantAddresses on tenant.Id equals tenantAddress.TenantId into tenantAddressesJoin
                    join tenantContacts in Context.TenantContacts on tenant.Id equals tenantContacts.TenantId into tenantContactsJoin
                    join tenantTenancies in Context.TenantTenancies on tenant.Id equals tenantTenancies.TenantId into tenantTenanciesJoin
                    from tt in tenantTenanciesJoin
                    join tenancies in Context.Tenancies on tt.TenancyId equals tenancies.Id
                    join propertyDetails in Context.PropertyDetails on tenancies.PropertyDetailsId equals propertyDetails.Id
                    join aup in Context.ApplicationUserPortfolios on propertyDetails.PortfolioId equals aup.PortfolioId
                    where aup.AgencyId == agencyId && !tenancies.IsDeleted && !propertyDetails.IsDeleted && !aup.IsDeleted && tenant.IsLeadTenant && !tenant.IsDeleted && (applicationUser.FirstName != null || applicationUser.LastName != null)
                    select new TenantViewModel
                    {
                        Id = tenant.Id,
                        FirstName = applicationUser.FirstName,
                        LastName = applicationUser.LastName,
                        IsLeadTenant = tenant.IsLeadTenant,
                        MainContactNumber = applicationUser.PhoneNumber,
                        EmailAddress = applicationUser.Email,
                        SecondaryContactNumber = applicationUser.SecondaryPhoneNumber,
                        Addresses = tenantAddressesJoin.Where(c => !c.IsDeleted).Select(c => new TenantAddressViewModel(!tenant.IsAdult, c)).ToList(),
                        Title = applicationUser.Title,
                        CompanyName = tenant.CompanyName,
                        IsAdult = tenant.IsAdult,
                        DateOfBirth = tenant.DateOfBirth,
                        MiddleName = applicationUser.MiddleName,
                        IsSmoker = tenant.IsSmoker,
                        HasPets = tenant.HasPets,
                        WorkContactNumber = tenant.WorkContactNumber,
                        PassportReference = tenant.PassportReference,
                        Occupation = tenant.Occupation,
                        WorkAddress = tenant.WorkAddress,
                        AdditionalInformation = tenant.AdditionalInformation,
                        DrivingLicenseReference = tenant.DrivingLicenseReference,
                        Contacts = tenantContactsJoin.Where(c => !c.IsDeleted).Select(c => new TenantContactViewModel(c)).ToList()
                    })
                .ToListAsync();
        }

        public async Task<TenantViewModel> GetTenantByIdAsync(Guid tenantId)
        {
            return await (from tenant in Context.Tenants.AsNoTracking()
                    join applicationUser in Context.Users on tenant.ApplicationUserId equals applicationUser.Id
                    join tenantAddresses in Context.TenantAddresses on tenant.Id equals tenantAddresses.TenantId into tenantAddressesJoin
                    join tenantContacts in Context.TenantContacts on tenant.Id equals tenantContacts.TenantId into tenantContactsJoin
                    where tenant.Id == tenantId && !tenant.IsDeleted
                    select new TenantViewModel
                    {
                        Id = tenant.Id,
                        FirstName = applicationUser.FirstName,
                        LastName = applicationUser.LastName,
                        IsLeadTenant = tenant.IsLeadTenant,
                        MainContactNumber = applicationUser.PhoneNumber,
                        EmailAddress = applicationUser.Email,
                        SecondaryContactNumber = applicationUser.SecondaryPhoneNumber,
                        Addresses = tenantAddressesJoin.Where(c => !c.IsDeleted).Select(c => new TenantAddressViewModel(!tenant.IsAdult, c)).ToList(),
                        Title = applicationUser.Title,
                        CompanyName = tenant.CompanyName,
                        IsAdult = tenant.IsAdult,
                        DateOfBirth = tenant.DateOfBirth,
                        MiddleName = applicationUser.MiddleName,
                        IsSmoker = tenant.IsSmoker,
                        HasPets = tenant.HasPets,
                        WorkContactNumber = tenant.WorkContactNumber,
                        PassportReference = tenant.PassportReference,
                        Occupation = tenant.Occupation,
                        WorkAddress = tenant.WorkAddress,
                        AdditionalInformation = tenant.AdditionalInformation,
                        DrivingLicenseReference = tenant.DrivingLicenseReference,
                        Contacts = tenantContactsJoin.Where(c => !c.IsDeleted).Select(c => new TenantContactViewModel(c)).ToList()
                    })
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