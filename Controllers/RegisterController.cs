namespace Landlords.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels;
    using Model.Database;

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
            IdentityResult registrationResult = null;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = user.Email = data.EmailAddress;
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;

                registrationResult = await _userRepository.Create(user, data.Password);
                if (registrationResult.Succeeded)
                {
                    // TODO
                    await _userRepository.AddToRole(user, ApplicationRoles.Landlord);
                    return Ok();
                }
            }

            return BadRequest(new
            {
                Errors = ModelState.ErrorCount > 0 ? ModelState.ToErrorCollection() : registrationResult.Errors.ToGeneric().ToErrorCollection()
            });
        }
    }
}