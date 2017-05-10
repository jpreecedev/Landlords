namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class PermissionsController : Controller
    {
        private readonly IPermissionsDataProvider _permissionsDataProvider;

        public PermissionsController(IPermissionsDataProvider permissionsDataProvider)
        {
            _permissionsDataProvider = permissionsDataProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _permissionsDataProvider.GetPermissionsAsync(User.GetUserId()));
        }
    }
}