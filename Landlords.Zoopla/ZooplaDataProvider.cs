namespace Landlords.Zoopla
{
    using Landlords.Shared;

    public class ZooplaDataProvider : IExternalDataProvider
    {
        public ZooplaDataProvider()
        {
            Configuration = new ZooplaConfiguration
            {
                ApiKey = "v389wnqzaayyd5zetgwgz45s"
            };
        }

        public IExternalProviderConfiguration Configuration { get; }
    }
}