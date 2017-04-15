namespace Landlords.Identity
{
    using System.Collections.Generic;
    using IdentityServer4.Models;

    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "landlords",
                    ClientName = "Example Client Credentials Client Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("landlords".Sha256())
                    },
                    AllowedScopes = new List<string> {"LandlordsAPI.Read"}
                }
            };
        }
    }
}