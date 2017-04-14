namespace Landlords.Identity
{
    using IdentityServer4.Models;
    using System.Collections.Generic;

    public class ApiResources
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "LandlordsAPI",
                    DisplayName = "Landlords API",
                    Description = "Landlords API Access",
                    //UserClaims = new List<string> {"role"},
                    //ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                    Scopes = new List<Scope>
                    {
                        new Scope("LandlordsAPI.Read"),
                        new Scope("LandlordsAPI.Write")
                    }
                }
            };
        }
    }
}