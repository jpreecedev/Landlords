namespace Landlords.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Model.Validation;

    public class TenantViewModel
    {
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