namespace Landlords.ViewModels
{
    using Model.DataTypes;
    using System.Collections.Generic;

    public class StartTenancyJourneyViewModel
    {
        public ICollection<TenantViewModel> Tenants { get; set; }

        public TenancyViewModel Tenancy { get; set; }

        public string[] DefaultCounties { get; } = Counties.GetDefaultCounties();

        public string[] DefaultCountries { get; } = Countries.GetDefaultCountries();

        public string[] DefaultTenancyTypes { get; } = TenancyTypes.GetDefaultTenancyTypes();

        public string[] DefaultRentalFrequencies { get; } = RentalFrequencies.GetDefaultRentalFrequencies();
    }
}