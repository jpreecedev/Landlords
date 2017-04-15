namespace Landlords.Jwt
{
    using System.Security.Claims;
    using Database;

    public class JwtIdentity
    {
        public ClaimsIdentity ClaimsIdentity { get; set; }
        public ApplicationUser User { get; set; }
    }
}