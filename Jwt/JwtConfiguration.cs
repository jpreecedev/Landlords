﻿namespace Landlords.Jwt
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Secret { get; set; }
    }
}