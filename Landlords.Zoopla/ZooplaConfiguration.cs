namespace Landlords.Zoopla
{
    using Landlords.Shared;

    public class ZooplaConfiguration : IExternalProviderConfiguration
    {
        public string ApiKey { get; set; }
    }
}