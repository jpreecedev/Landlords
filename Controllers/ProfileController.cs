namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels;
    using Landlords.Permissions;
    using Model;

    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Permission(Permissions_P.ViewId, Permissions_P.ViewRouteId, Permissions_P.ViewDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userRepository.GetProfileAsync(User.GetUserId()));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_P.UpdateId, Permissions_P.UpdateRouteId, Permissions_P.UpdateDescription)]
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