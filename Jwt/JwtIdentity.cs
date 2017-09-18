namespace Landlords.Jwt
{
    using System.Security.Claims;
    using Landlords.ViewModels;

    public class JwtIdentity
    {
        public ClaimsIdentity ClaimsIdentity { get; set; }
        public ApplicationUserViewModel User { get; set; }
    }
}