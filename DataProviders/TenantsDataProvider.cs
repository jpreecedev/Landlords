namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;
    using Model.Database;
    using Model.Entities;

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

        public async Task UpdateAsync(Guid portfolioId, ICollection<TenantViewModel> tenants)
        {
            foreach (var tenant in tenants)
            {
                if (tenant.IsNew)
                {
                    await CreateAsync(portfolioId, tenant);
                }
                else
                {
                    await UpdateAsync(portfolioId, tenant);
                }
            }
        }

        public async Task UpdateAsync(Guid portfolioId, TenantViewModel tenant)
        {
            var existingEntity = await Context.Tenants.Include(x => x.Addresses)
                .Include(x => x.Contacts)
                .SingleAsync(c => c.Id == tenant.Id && !c.IsDeleted);
            
            existingEntity.MapFrom(tenant);

            var applicationUser = await Context.Users.SingleAsync(c => c.Id == existingEntity.ApplicationUserId);
            UpdateApplicationUser(applicationUser, tenant);

            if (tenant.Addresses != null && tenant.Addresses.Any())
            {
                foreach (var address in existingEntity.Addresses)
                {
                    var updatedAddress = tenant.Addresses.FirstOrDefault(c => c.Id == address.Id);
                    if (updatedAddress != null)
                    {
                        address.MapFrom(updatedAddress);
                        if (updatedAddress.IsDeleted)
                        {
                            address.Deleted = DateTime.Now;
                        }
                    }
                }
                existingEntity.Addresses.AddRange(CreateTenantAddresses(existingEntity, tenant.Addresses.Where(c => c.Id.IsDefault()).ToList()));
            }
            if (tenant.Contacts != null && tenant.Contacts.Any())
            {
                foreach (var existingEntityContact in existingEntity.Contacts)
                {
                    var updatedEntity = tenant.Contacts.FirstOrDefault(c => c.Id == existingEntityContact.Id);
                    if (updatedEntity != null)
                    {
                        existingEntityContact.MapFrom(updatedEntity);
                        if (updatedEntity.IsDeleted)
                        {
                            existingEntityContact.Deleted = DateTime.Now;
                        }
                    }
                }
                existingEntity.Contacts.AddRange(CreateTenantContacts(existingEntity, tenant.Contacts.Where(c => c.Id.IsDefault()).ToList()));
            }

            Context.Tenants.Update(existingEntity);
            await Context.SaveChangesAsync();
        }

        public async Task<TenantViewModel> CreateAsync(Guid portfolioId, TenantViewModel tenant)
        {
            var tenantUser = await Context.Users.FirstOrDefaultAsync(c => c.Email == tenant.EmailAddress);
            if (tenantUser == null && tenant.IsAdult)
            {
                tenantUser = new ApplicationUser
                {
                    UserName = tenant.EmailAddress,
                    Email = tenant.EmailAddress,
                    EmailConfirmed = false,
                    FirstName = tenant.FirstName,
                    MiddleName = tenant.MiddleName,
                    LastName = tenant.LastName,
                    PhoneNumber = tenant.MainContactNumber,
                    SecondaryPhoneNumber = tenant.SecondaryContactNumber,
                    Title = tenant.Title
                };

                await Context.Users.AddAsync(tenantUser);
                await Context.SaveChangesAsync();
            }

            var entity = new Tenant
            {
                ApplicationUser = tenantUser,
                AdditionalInformation = tenant.AdditionalInformation,
                CompanyName = tenant.CompanyName,
                Created = DateTime.Now,
                DateOfBirth = tenant.DateOfBirth,
                DrivingLicenseReference = tenant.DrivingLicenseReference,
                HasPets = tenant.HasPets,
                IsSmoker = tenant.IsSmoker,
                EmploymentType = tenant.EmploymentType,
                Occupation = tenant.Occupation,
                PassportReference = tenant.PassportReference,
                WorkAddress = tenant.WorkAddress,
                WorkContactNumber = tenant.WorkContactNumber,
                IsLeadTenant = tenant.IsLeadTenant,
                IsAdult = tenant.IsAdult
            };

            entity.Addresses = CreateTenantAddresses(entity, tenant.Addresses);
            entity.Contacts = CreateTenantContacts(entity, tenant.Contacts);

            await Context.Tenants.AddAsync(entity);
            await Context.SaveChangesAsync();

            return new TenantViewModel(entity, tenantUser);
        }

        private ICollection<TenantAddress> CreateTenantAddresses(Tenant tenant, ICollection<TenantAddressViewModel> addresses)
        {
            if (addresses == null)
            {
                return null;
            }

            var result = new List<TenantAddress>();
            foreach (var address in addresses)
            {
                var entity = new TenantAddress
                {
                    Tenant = tenant,
                    Country = address.Country,
                    CountyOrRegion = address.CountyOrRegion,
                    Created = DateTime.Now,
                    Postcode = address.Postcode,
                    Street = address.Street,
                    TownOrCity = address.TownOrCity,
                    YearsAtAddress = address.YearsAtAddress,
                    MonthsAtAddress = address.MonthsAtAddress
                };

                result.Add(entity);
            }
            return result;
        }

        private ICollection<TenantContact> CreateTenantContacts(Tenant tenant, ICollection<TenantContactViewModel> contacts)
        {
            if (contacts == null)
            {
                return null;
            }

            var result = new List<TenantContact>();
            foreach (var contact in contacts)
            {
                var entity = new TenantContact
                {
                    Comments = contact.Comments,
                    Created = DateTime.Now,
                    MainContactNumber = contact.MainContactNumber,
                    Name = contact.Name,
                    Relationship = contact.Relationship,
                    SecondaryContactNumber = contact.SecondaryContactNumber,
                    Tenant = tenant
                };

                result.Add(entity);
            }
            return result;
        }

        private void UpdateApplicationUser(ApplicationUser applicationUser, TenantViewModel tenant)
        {
            applicationUser.Email = applicationUser.UserName = tenant.EmailAddress;
            applicationUser.NormalizedEmail = applicationUser.NormalizedUserName = tenant.EmailAddress.ToUpper();
            applicationUser.PhoneNumber = tenant.MainContactNumber;
            applicationUser.SecondaryPhoneNumber = tenant.SecondaryContactNumber;
            applicationUser.MapFrom(tenant);
        }
    }
}