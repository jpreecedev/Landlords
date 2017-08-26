namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Core;
    using Model.Database;
    using Model.Validation;
    using Model.Entities;

    public class TenantViewModel : IValidatableObject
    {
        public TenantViewModel()
        {
            
        }

        public TenantViewModel(Tenant tenant, ApplicationUser applicationUser)
        {
            if (tenant == null)
            {
                return;
            }

            Id = tenant.Id;
            Title = applicationUser.Title;
            FirstName = applicationUser.FirstName;
            MiddleName = applicationUser.MiddleName;
            LastName = applicationUser.LastName;
            DateOfBirth = tenant.DateOfBirth;
            MainContactNumber = applicationUser.PhoneNumber;
            SecondaryContactNumber = applicationUser.SecondaryPhoneNumber;
            EmailAddress = applicationUser.Email;
            CompanyName = tenant.CompanyName;
            WorkContactNumber = tenant.WorkContactNumber;
            WorkAddress = tenant.WorkAddress;
            DrivingLicenseReference = tenant.DrivingLicenseReference;
            PassportReference = tenant.PassportReference;
            IsSmoker = tenant.IsSmoker;
            HasPets = tenant.HasPets;
            AdditionalInformation = tenant.AdditionalInformation;
            IsLeadTenant = tenant.IsLeadTenant;
            IsAdult = tenant.IsAdult;
            Occupation = tenant.Occupation;
            EmploymentType = tenant.EmploymentType;

            if (tenant.Addresses != null)
            {
                Addresses = tenant.Addresses.Select(c => new TenantAddressViewModel(!tenant.IsAdult, c)).ToList();
            }
            if (tenant.Contacts != null)
            { 
                Contacts = tenant.Contacts.Select(c => new TenantContactViewModel(c)).ToList();
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

        [RequiredIfTrue("IsAdult")]
        public ICollection<TenantAddressViewModel> Addresses { get; set; }

        [RequiredIfTrue("IsAdult")]
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

        [Display(Name = "Employment type")]
        [RequiredIfTrue("IsAdult"), MinLength(2)]
        public string EmploymentType { get; set; }

        public bool IsSmoker { get; set; }

        public bool HasPets { get; set; }

        public string AdditionalInformation { get; set; }

        public bool IsLeadTenant { get; set; }

        public bool IsAdult { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsNew
        {
            get { return Id.IsDefault(); }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsAdult)
            {
                if (Contacts == null || Contacts.Count < 1)
                {
                    yield return new ValidationResult("All tenants must have at least one contact");
                }

                if (Addresses == null || Addresses.Count < 1)
                {
                    yield return new ValidationResult("All tenants must have at least one previous address");
                }
            }
        }
    }
}