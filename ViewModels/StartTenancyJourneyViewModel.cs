namespace Landlords.ViewModels
{
    using Model.DataTypes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Tenants == null)
            {
                yield return new ValidationResult("There must be at least 1 tenant");
            }
            if (Tenants.Count(c => c.IsAdult && c.IsLeadTenant) < 1)
            {
                yield return new ValidationResult("There must be at least 1 lead adult tenant");
            }
        }
    }
}