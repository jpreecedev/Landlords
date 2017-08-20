namespace Landlords.ViewModels
{
    using System;
    using Model.DataTypes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Core;

    public class StartTenancyJourneyViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "There must be at least 1 tenant")]
        public ICollection<TenantViewModel> Tenants { get; set; }

        [Required(ErrorMessage = "There must be a tenancy")]
        public TenancyViewModel Tenancy { get; set; }

        public LLDataType[] DefaultCounties { get; } = Counties.GetDefaultCounties();

        public LLDataType[] DefaultCountries { get; } = Countries.GetDefaultCountries();

        public LLDataType[] DefaultTenancyTypes { get; } = TenancyTypes.GetDefaultTenancyTypes();

        public LLDataType[] DefaultRentalFrequencies { get; } = RentalFrequencies.GetDefaultRentalFrequencies();

        public LLDataType[] DefaultTitles { get; } = Titles.GetDefaultTitles();

        public LLDataType[] DefaultEmploymentTypes { get; } = EmploymentTypes.GetDefaultEmploymentTypes();

        public LLDataType[] DefaultTenantContactTypes { get; } = TenantContactTypes.GetDefaultTenantContactTypes();
        
        public bool IsNew()
        {
            return Tenancy.Id.IsDefault();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Tenants == null || Tenants.Count == 0)
            {
                yield return new ValidationResult("There must be at least 1 tenant");
            }
            else if (Tenants.Count(c => c.IsAdult && c.IsLeadTenant) < 1)
            {
                yield return new ValidationResult("There must be at least 1 lead adult tenant");
            }

            if (Tenants.Any(c => c.Contacts == null || c.Contacts.Count < 1))
            {
                yield return new ValidationResult("All tenants must have at least one contact");
            }

            if (Tenants.Any(c => c.Addresses == null || c.Addresses.Count < 1))
            {
                yield return new ValidationResult("All tenants must have at least one previous address");
            }

            foreach (var tenantViewModel in Tenants)
            {
                var start = DateTime.Now.Date.StartOfMonth();
                foreach (var tenantAddressViewModel in tenantViewModel.Addresses)
                {
                    start = start.Date.AddYears(tenantAddressViewModel.YearsAtAddress * -1).AddMonths(tenantAddressViewModel.MonthsAtAddress * -1);
                }

                if (start > DateTime.Now.Date.StartOfMonth().AddYears(-3))
                {
                    yield return new ValidationResult("Each tenant must supply at least 3 years address details");
                }
            }
        }
    }
}