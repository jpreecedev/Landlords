namespace Landlords.Jwt
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;

    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly LLDbContext _context;
        private readonly ApplicationUserManager _userManager;

        public TokenValidatorService(LLDbContext context, ApplicationUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task ValidateAsync(TokenValidatedContext context)
        {
            var userPrincipal = context.Principal;

            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail("This is not our issued token. It has no claims.");
                return;
            }

            var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
            if (serialNumberClaim == null)
            {
                context.Fail("This is not our issued token. It has no serial.");
                return;
            }

            var userIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            if (!int.TryParse(userIdString, out var userId))
            {
                context.Fail("This is not our issued token. It has no user-id.");
                return;
            }

            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var user = await _context.Users.FirstOrDefaultAsync(u => string.Equals(u.Email.Replace(" ", "").Trim(), username, StringComparison.CurrentCultureIgnoreCase));
            if (user == null)
            {
                context.Fail("Unable to locate user");
            }
        }
    }
}