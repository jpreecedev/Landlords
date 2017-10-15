using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Landlords.Jwt
{
    using Microsoft.IdentityModel.Tokens;
    using Model.Database;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    public interface ITokenStoreService
    {
        Task<(string accessToken, string refreshToken)> CreateJwtTokens(string username, string password);
    }

    public class TokenStoreService : ITokenStoreService
    {
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;

        public TokenStoreService(IOptionsSnapshot<BearerTokensOptions> configuration)
        {
            _configuration = configuration;
        }

        public async Task<(string accessToken, string refreshToken)> CreateJwtTokens(string username, string password)
        {
            var now = DateTimeOffset.UtcNow;
            var accessTokenExpiresDateTime = now.AddMinutes(_configuration.Value.AccessTokenExpirationMinutes);
            var refreshTokenExpiresDateTime = now.AddMinutes(_configuration.Value.RefreshTokenExpirationMinutes);
            var accessToken = await createAccessTokenAsync(user, accessTokenExpiresDateTime.UtcDateTime).ConfigureAwait(false);
            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");

            await AddUserTokenAsync(user, refreshToken, accessToken, refreshTokenExpiresDateTime, accessTokenExpiresDateTime).ConfigureAwait(false);
            await _uow.SaveChangesAsync().ConfigureAwait(false);

            return (accessToken, refreshToken);
        }

        public async Task AddUserTokenAsync(UserToken userToken)
        {
            await InvalidateUserTokensAsync(userToken.UserId).ConfigureAwait(false);
            _tokens.Add(userToken);
        }

        public async Task AddUserTokenAsync(
            User user, string refreshToken, string accessToken,
            DateTimeOffset refreshTokenExpiresDateTime, DateTimeOffset accessTokenExpiresDateTime)
        {
            var token = new UserToken
            {
                UserId = user.Id,
                // Refresh token handles should be treated as secrets and should be stored hashed
                RefreshTokenIdHash = _securityService.GetSha256Hash(refreshToken),
                AccessTokenHash = _securityService.GetSha256Hash(accessToken),
                RefreshTokenExpiresDateTime = refreshTokenExpiresDateTime,
                AccessTokenExpiresDateTime = accessTokenExpiresDateTime
            };
            await AddUserTokenAsync(token).ConfigureAwait(false);
        }

        public async Task InvalidateUserTokensAsync(int userId)
        {
            var userTokens = await _tokens.Where(x => x.UserId == userId).ToListAsync().ConfigureAwait(false);
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
        }

        private async Task<string> createAccessTokenAsync(ApplicationUser user, DateTime expires)
        {
            var claims = new List<Claim>
            {
                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer),
                // Issued at
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("DisplayName", user.DisplayName),
                // to invalidate the cookie
                new Claim(ClaimTypes.SerialNumber, user.SerialNumber),
                // custom data
                new Claim(ClaimTypes.UserData, user.Id.ToString())
            };

            // add roles
            var roles = await _rolesService.FindUserRolesAsync(user.Id).ConfigureAwait(false);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration.Value.Issuer,
                audience: _configuration.Value.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
