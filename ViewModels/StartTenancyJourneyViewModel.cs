namespace Landlords.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Model.DataTypes;

    public class StartTenancyJourneyViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "There must be at least 1 tenant")]
        public ICollection<TenantViewModel> Tenants { get; set; }

        [Required(ErrorMessage = "There must be a tenancy")]
        public TenancyViewModel Tenancy { get; set; }

        public string[] DefaultCounties { get; } = Counties.GetDefaultCounties();

        public string[] DefaultCountries { get; } = Countries.GetDefaultCountries();

        public string[] DefaultTenancyTypes { get; } = TenancyTypes.GetDefaultTenancyTypes();

        public string[] DefaultRentalFrequencies { get; } = RentalFrequencies.GetDefaultRentalFrequencies();

        public string[] DefaultTitles { get; } = Titles.GetDefaultTitles();

        public string[] DefaultEmploymentTypes { get; } = EmploymentTypes.GetDefaultEmploymentTypes();

        public string[] DefaultTenantContactTypes { get; } = TenantContactTypes.GetDefaultTenantContactTypes();

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