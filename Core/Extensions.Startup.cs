namespace Landlords.Core
{
    using Jwt;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Http;

    public static class StartupExtensions
    {
        private static IOptions<JwtConfiguration> _jwtConfiguration;
        private static SecurityKey _key;

        public static TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateIssuer = true,
                ValidIssuer = _jwtConfiguration.Value.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtConfiguration.Value.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

        public static IApplicationBuilder UseJwt(this IApplicationBuilder app, IOptions<JwtConfiguration> jwtConfiguration, SecurityKey key)
        {
            _jwtConfiguration = jwtConfiguration;
            _key = key;

            return app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = GetTokenValidationParameters()
            });
        }

        public static IApplicationBuilder UseXsrf(this IApplicationBuilder app, IAntiforgery antiforgery)
        {
            return app.Use((context, next) =>
            {
                var tokens = antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions {HttpOnly = false});
                return next.Invoke();
            });
        }
    }
}