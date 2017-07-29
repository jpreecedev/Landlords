namespace Landlords.Jwt
{
    using System.Security.Claims;

    public class JwtValidationResult
    {
        public JwtValidationResult(ClaimsPrincipal user)
        {
            User = user;
        }

        public ClaimsPrincipal User { get; }

        public bool IsValid => User != null;
    }
}