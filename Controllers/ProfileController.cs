namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels;
    using Landlords.Permissions;
    using Model.Permissions;

    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Permission(Profile.ViewId, Profile.ViewRouteId, Profile.ViewDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userRepository.GetProfileAsync(User.GetUserId()));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Profile.UpdateId, Profile.UpdateRouteId, Profile.UpdateDescription)]
        public async Task<IActionResult> Post([FromBody] ProfileViewModel value)
        {
            if (ModelState.IsValid)
            {
                var userId = User.GetUserId();

                if (value.UserId.IsDefault() || userId != value.UserId)
                    return BadRequest("Unable to validate payload");

                await _userRepository.UpdateAsync(User.GetUserId(), value);
                return Ok();
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}