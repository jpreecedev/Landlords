namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Model.Entities;
    using Model.Validation;

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
            Title = tenant.Title;
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
            IsAdult = tenant.IsAdult;
            IsLeadTenant = tenant.IsLeadTenant;

            if (tenantAddresses != null)
            {
                Addresses = tenantAddresses.Select(c => new TenantAddressViewModel(!IsAdult, c)).ToList();
            }
            if (tenantContacts != null)
            {
                Contacts = tenantContacts.Select(c => new TenantContactViewModel(c)).ToList();
            }
        }

        public Guid Id { get; set; }

        [Required, MinLength(2)]
        public string Title { get; set; }

        [Display(Name = "First name")]
        [Required, MinLength(2)]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        [Required, MinLength(2)]
        public string LastName { get; set; }

        [LLDate]
        public DateTime DateOfBirth { get; set; }

        [RequiredIfTrue("IsAdult"), EnsureMinimumElements(1)]
        public ICollection<TenantAddressViewModel> Addresses { get; set; }

        [RequiredIfTrue("IsAdult"), EnsureMinimumElements(1)]
        public ICollection<TenantContactViewModel> Contacts { get; set; }
        
        [Display(Name = "Main contact number")]
        [RequiredIfTrue("IsAdult"), MinLength(2)]
        public string MainContactNumber { get; set; }
        
        public string SecondaryContactNumber { get; set; }

        [Display(Name = "Email address")]
        [RequiredIfTrue("IsAdult"), EmailAddress]
        public string EmailAddress { get; set; }

        [RequiredIfTrue("IsAdult"), MinLength(2)]
        public string Occupation { get; set; }

        [Display(Name = "Company name")]
        [RequiredIfTrue("IsAdult"), MinLength(2)]
        public string CompanyName { get; set; }

        [Display(Name = "Work contact number")]
        [RequiredIfTrue("IsAdult"), MinLength(2)]
        public string WorkContactNumber { get; set; }

        [Display(Name = "Work address")]
        [RequiredIfTrue("IsAdult"), MinLength(2)]
        public string WorkAddress { get; set; }

        [Display(Name = "Driving license reference")]
        [RequiredIfTrue("IsAdult"), MinLength(2)]
        public string DrivingLicenseReference { get; set; }

        [Display(Name = "Passport reference")]
        [RequiredIfTrue("IsAdult"), MinLength(2)]
        public string PassportReference { get; set; }

        public bool IsSmoker { get; set; }

        public bool HasPets { get; set; }

        public string AdditionalInformation { get; set; }

        public bool IsLeadTenant { get; set; }

        public bool IsAdult { get; set; }
    }
}