namespace Landlords.Jwt
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

    public interface ITokenValidatorService
    {
        Task ValidateAsync(TokenValidatedContext context);
    }
}