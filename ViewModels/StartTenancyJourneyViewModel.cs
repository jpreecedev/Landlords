namespace Landlords.ViewModels
{
    using Model.DataTypes;

    public class StartTenancyJourneyViewModel
    {
        public string[] DefaultCountries { get; } = Countries.GetDefaultCountries();

        public string[] DefaultTenancyTypes { get; } = TenancyTypes.GetDefaultTenancyTypes();

        public string[] DefaultRentalFrequencies { get; } = RentalFrequencies.GetDefaultRentalFrequencies();
    }
}