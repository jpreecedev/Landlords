namespace Landlords.Core
{
    using Jwt;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Landlords.Services;

    public static class StartupExtensions
    {
        public static JwtValidationResult ToJwtValidationResult(this string accessToken, IConfiguration configuration)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new JwtValidationResult(handler.ValidateToken(accessToken, GetTokenValidationParameters(configuration), out SecurityToken token));
        }

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["BearerTokens:Key"])),
                ValidateIssuer = true,
                ValidIssuer = configuration["BearerTokens:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["BearerTokens:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

        public static AuthenticationBuilder UseJwt(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["BearerTokens:Issuer"],
                        ValidAudience = configuration["BearerTokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["BearerTokens:Key"])),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                            return tokenValidatorService.ValidateAsync(context);
                        },
                    };
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