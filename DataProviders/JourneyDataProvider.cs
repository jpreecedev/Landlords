namespace Landlords.DataProviders
{
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Model.Entities;
    using Model.Database;

    public class JourneyDataProvider : BaseDataProvider, IJourneyDataProvider
    {
        public JourneyDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task CreateTenancyAsync(Guid portfolioId, StartTenancyJourneyViewModel viewModel)
        {
            //Untested

            var tenants = await CreateTenantsAsync(viewModel);
            await Context.Tenants.AddRangeAsync(tenants);

            var tenancy = CreateTenancy(viewModel);
            await Context.AddAsync(tenancy);

            var tenantTenancy = CreateTenantTenancy(tenants, tenancy);
            await Context.AddRangeAsync(tenantTenancy);

            await Context.SaveChangesAsync();
        }

        private async Task<ICollection<Tenant>> CreateTenantsAsync(StartTenancyJourneyViewModel viewModel)
        {
            var result = new List<Tenant>();
            foreach (var tenant in viewModel.Tenants)
            {
                var tenantUser = await Context.Users.FirstOrDefaultAsync(c => c.Email == tenant.EmailAddress);
                if (tenantUser == null)
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

                result.Add(entity);
            }
            return result;
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

        private Tenancy CreateTenancy(StartTenancyJourneyViewModel viewModel)
        {
            var tenancy = viewModel.Tenancy;
            if (tenancy == null)
            {
                throw new InvalidOperationException("Tenancy is required");
            }

            return new Tenancy
            {
                PropertyDetailsId = tenancy.PropertyDetailsId,
                Created = DateTime.Now,
                StartDate = tenancy.StartDate,
                EndDate = tenancy.EndDate,
                RentalAmount = tenancy.RentalAmount,
                RentalFrequency = tenancy.RentalFrequency,
                RentalPaymentReference = tenancy.RentalPaymentReference,
                TenancyType = tenancy.TenancyType
            };
        }

        private ICollection<TenantTenancy> CreateTenantTenancy(ICollection<Tenant> tenants, Tenancy tenancy)
        {
            return tenants.Select(c => new TenantTenancy
            {
                Created = DateTime.Now,
                Tenancy = tenancy,
                Tenant = c
            })
            .ToList();
        }
    }
}