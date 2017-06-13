namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.DataTypes;
    using Model.Entities;

    public class TenantViewModel
    {
        public TenantViewModel()
        {
        }

        public TenantViewModel(Tenant tenant, ICollection<TenantAddress> tenantAddresses)
        {
            if (tenant == null)
            {
                return;
            }

            Id = tenant.Id;
            FirstName = tenant.FirstName;
            MiddleName = tenant.MiddleName;
            LastName = tenant.LastName;
            DateOfBirth = tenant.DateOfBirth;
            MainContactNumber = tenant.MainContactNumber;
            SecondaryContactNumber = tenant.SecondaryContactNumber;
            EmailAddress = tenant.EmailAddress;
            PortfolioId = tenant.PortfolioId;

            if (tenantAddresses != null)
            {
                Addresses = tenantAddresses.Select(c => new TenantAddressViewModel(c)).ToList();
            }
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<TenantAddressViewModel> Addresses { get; set; }

        public string MainContactNumber { get; set; }

        public string SecondaryContactNumber { get; set; }

        public string EmailAddress { get; set; }

        public Guid PortfolioId { get; set; }

        public string[] DefaultCountries { get; } = Countries.GetDefaultCountries();
    }
}