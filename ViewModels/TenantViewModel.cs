namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Entities;

    public class TenantViewModel
    {
        public TenantViewModel()
        {
        }

        public TenantViewModel(Tenant tenant, ICollection<TenantAddress> tenantAddresses, ICollection<TenantContact> tenantContacts)
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
            Occupation = tenant.Occupation;
            CompanyName = tenant.CompanyName;
            WorkContactNumber = tenant.WorkContactNumber;
            WorkAddress = tenant.WorkAddress;
            DrivingLicenseReference = tenant.DrivingLicenseReference;
            PassportReference = tenant.PassportReference;
            IsSmoker = tenant.IsSmoker;
            HasPets = tenant.HasPets;
            AdditionalInformation = tenant.AdditionalInformation;

            if (tenantAddresses != null)
            {
                Addresses = tenantAddresses.Select(c => new TenantAddressViewModel(c)).ToList();
            }
            if (tenantContacts != null)
            {
                Contacts = tenantContacts.Select(c => new TenantContactViewModel(c)).ToList();
            }
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<TenantAddressViewModel> Addresses { get; set; }

        public ICollection<TenantContactViewModel> Contacts { get; set; }

        public string MainContactNumber { get; set; }

        public string SecondaryContactNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Occupation { get; set; }

        public string CompanyName { get; set; }

        public string WorkContactNumber { get; set; }

        public string WorkAddress { get; set; }

        public string DrivingLicenseReference { get; set; }

        public string PassportReference { get; set; }

        public bool IsSmoker { get; set; }

        public bool HasPets { get; set; }

        public string AdditionalInformation { get; set; }

        public bool IsLeadTenant { get; set; }

        public bool IsAdult { get; set; }
    }
}