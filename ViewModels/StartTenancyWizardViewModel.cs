namespace Landlords.ViewModels
{
    using Model.DataTypes;

    public class StartTenancyWizardViewModel
    {
        public string[] DefaultTenancyTypes { get; } = TenancyTypes.GetDefaultTenancyTypes();
    }
}