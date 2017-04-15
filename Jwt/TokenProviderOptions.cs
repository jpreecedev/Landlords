namespace Landlords.Jwt
{
    using System;
    using Microsoft.IdentityModel.Tokens;

    public class TokenProviderOptions
    {
        public string TokenPath { get; set; } = "/oauth/token";
        
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(30);

        public SigningCredentials SigningCredentials { get; set; }
    }
}