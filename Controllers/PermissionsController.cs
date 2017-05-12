namespace Landlords.Controllers
{
    using System;
    using System.Linq;
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

        [HttpGet("{userId}")]
        [Permission(Permissions_PE.GetById, Permissions_PE.GetByRouteId, Permissions_PE.GetByIdDescription)]
        public async Task<IActionResult> Get(Guid userId)
        {
            var permissions = await _permissionsDataProvider.GetUserPermissionsAsync(userId);
            var grouped = permissions.GroupBy(c => c.RouteId.Substring(0, c.RouteId.IndexOf("_")));

            return Ok(grouped.Select(c => new
            {
                Key = c.Key,
                Items = c
            }));
        }

        [HttpGet("all")]
        [Permission(Permissions_PE.ListId, Permissions_PE.ListRouteId, Permissions_PE.ListDescription)]
        public async Task<IActionResult> GetAll()
        {
            var permissions = await _permissionsDataProvider.GetPermissionsAsync();
            var grouped = permissions.GroupBy(c => c.RouteId.Substring(0, c.RouteId.IndexOf("_")));

            return Ok(grouped.Select(c => new
            {
                Key = c.Key,
                Items = c
            }));
        }

        [HttpGet("users")]
        [Permission(Permissions_PE.UsersId, Permissions_PE.UsersRouteId, Permissions_PE.UsersDescription)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _permissionsDataProvider.GetUsersAsync());
        }

        [HttpPost]
        [Permission(Permissions_PE.AddId, Permissions_PE.AddRouteId, Permissions_PE.AddDescription)]
        public async Task<IActionResult> Post(Guid userId, Guid permissionId)
        {
            await _permissionsDataProvider.AddPermissionAsync(userId, permissionId);
            return Ok();
        }

        [HttpDelete]
        [Permission(Permissions_PE.DeleteId, Permissions_PE.DeleteRouteId, Permissions_PE.DeleteDescription)]
        public async Task<IActionResult> Delete(Guid userId, Guid permissionId)
        {
            await _permissionsDataProvider.RemovePermissionAsync(userId, permissionId);
            return Ok();
        }
    }
}