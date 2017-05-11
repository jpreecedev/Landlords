namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Permissions;
    using Model;

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
            return Ok(await _permissionsDataProvider.GetUserPermissionsAsync(User.GetUserId()));
        }

        [HttpGet("all")]
        [Permission(Permissions_PE.ListId, Permissions_PE.ListRouteId, Permissions_PE.ListDescription)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _permissionsDataProvider.GetPermissionsAsync());
        }
    }
}