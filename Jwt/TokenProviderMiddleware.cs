namespace Landlords.Jwt
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Landlords.Database;
    using System.Collections.Generic;
    using Core;
    using Landlords.ViewModels;

    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LLDbContext _dataContext;
        private readonly ApplicationUserManager _userManager;
        private readonly TokenProviderOptions _options;

        public TokenProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options, LLDbContext dataContext, ApplicationUserManager userManager)
        {
            _next = next;
            _dataContext = dataContext;
            _userManager = userManager;
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.TokenPath, StringComparison.Ordinal))
                return _next(context);

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
                || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var jwtIdentity = await GetIdentity(username, password);
            if (jwtIdentity == null)
            {


                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password".AsGenericError());
                return;
            }

            var now = DateTime.UtcNow;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new List<Claim>(jwtIdentity.ClaimsIdentity.Claims)
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.Ticks.ToString(), ClaimValueTypes.Integer64)
            };

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds,
                name = jwtIdentity.User.FirstName
            };

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private async Task<JwtIdentity> GetIdentity(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<JwtIdentity>(null);
            }

            var user = _dataContext.Users.FirstOrDefault(u => string.Equals(u.Email.Replace(" ", "").Trim(), username, StringComparison.CurrentCultureIgnoreCase));
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(LLClaimTypes.AgencyIdentifier, user.AgencyId.GetValueOrDefault().ToString())
                    };

                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var portfolios = _dataContext.ApplicationUserPortfolios.Where(c => c.UserId == user.Id);
                    foreach (var link in portfolios)
                    {
                        claims.Add(new Claim(LLClaimTypes.PortfolioIdentifier, link.PortfolioId.ToString()));
                    }

                    return await Task.FromResult(new JwtIdentity
                    {
                        User = new ApplicationUserViewModel(user),
                        ClaimsIdentity = new ClaimsIdentity(claims)
                    });
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<JwtIdentity>(null);
        }
    }
}