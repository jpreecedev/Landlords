namespace Landlords.ViewModels
{
    using Model.DataTypes;

    public class StartTenancyWizardViewModel
    {
        public string[] DefaultCountries { get; } = Countries.GetDefaultCountries();

        public string[] DefaultTenancyTypes { get; } = TenancyTypes.GetDefaultTenancyTypes();
    }
}