namespace Landlords.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels;

    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly IUserRepository _userRepository;

        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterUserViewModel data)
        {
            var user = new ApplicationUser();
            user.UserName = user.Email = data.EmailAddress;

            var result = await _userRepository.Create(user, data.Password);
            if (result.Succeeded)
            {
                await _userRepository.AddToRole(user, ApplicationRoles.Landlord);
                return Ok();
            }

            return BadRequest(string.Join(",", result.Errors));
        }
    }
}