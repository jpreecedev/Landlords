namespace Landlords.Shared
{
    public interface IExternalDataProvider
    {
        IExternalProviderConfiguration Configuration { get; }
    }
}