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
    using Model.Entities;

    public class JourneyDataProvider : BaseDataProvider, IJourneyDataProvider
    {
        public JourneyDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task CreateTenancyAsync(Guid portfolioId, StartTenancyJourneyViewModel viewModel)
        {
            //Untested

            var tenants = CreateTenants(viewModel);
            await Context.Tenants.AddRangeAsync(tenants);

            var tenancy = CreateTenancy(viewModel);
            await Context.AddAsync(tenancy);

            var tenantTenancy = CreateTenantTenancy(tenants, tenancy);
            await Context.AddAsync(tenantTenancy);

            await Context.SaveChangesAsync();
        }

        private ICollection<Tenant> CreateTenants(StartTenancyJourneyViewModel viewModel)
        {
            var result = new List<Tenant>();
            foreach (var tenant in viewModel.Tenants)
            {
                var entity = new Tenant
                {
                    AdditionalInformation = tenant.AdditionalInformation,
                    CompanyName = tenant.CompanyName,
                    Created = DateTime.Now,
                    DateOfBirth = tenant.DateOfBirth,
                    DrivingLicenseReference = tenant.DrivingLicenseReference,
                    EmailAddress = tenant.EmailAddress,
                    FirstName = tenant.FirstName,
                    MiddleName = tenant.MiddleName,
                    LastName = tenant.LastName,
                    HasPets = tenant.HasPets,
                    IsSmoker = tenant.IsSmoker,
                    MainContactNumber = tenant.MainContactNumber,
                    SecondaryContactNumber = tenant.SecondaryContactNumber,
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
                throw new ArgumentNullException(nameof(addresses));
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
                    TownOrCity = address.TownOrCity
                };

                result.Add(entity);
            }
            return result;
        }

        private ICollection<TenantContact> CreateTenantContacts(Tenant tenant, ICollection<TenantContactViewModel> contacts)
        {
            if (contacts == null)
            {
                throw new ArgumentNullException(nameof(contacts));
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

        private TenantTenancy CreateTenantTenancy(ICollection<Tenant> tenants, Tenancy tenancy)
        {
            return new TenantTenancy
            {
                Created = DateTime.Now,
                Tenancy = tenancy,
                Tenant = tenants.Single(c => c.IsLeadTenant)
            };
        }
    }
}