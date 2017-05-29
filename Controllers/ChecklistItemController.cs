namespace Landlords.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;

    [Route("api/[controller]")]
    public class ChecklistItemController : Controller
    {
        private readonly IChecklistItemDataProvider _checklistItemDataProvider;

        public ChecklistItemController(IChecklistItemDataProvider checklistItemDataProvider)
        {
            _checklistItemDataProvider = checklistItemDataProvider;
        }

        [HttpPost("completed")]
        [ValidateAntiForgeryToken]
        [Permission(Permissions_CI.ToggleCompletedId, Permissions_CI.ToggleCompletedRouteId, Permissions_CI.ToggleCompletedDescription)]
        public async Task<IActionResult> Completed(Guid checklistId, Guid checklistItemId, bool completed)
        {
            if (checklistId.IsDefault() || checklistItemId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _checklistItemDataProvider.ToggleChecklistItemCompletedAsync(User.GetUserId(), checklistId, checklistItemId, completed);
            return Ok();
        }

        [HttpPost("expanded")]
        [ValidateAntiForgeryToken]
        [Permission(Permissions_CI.ToggleExpandedId, Permissions_CI.ToggleExpandedRouteId, Permissions_CI.ToggleExpandedDescription)]
        public async Task<IActionResult> Expanded(Guid checklistId, Guid checklistItemId, bool expanded)
        {
            if (checklistId.IsDefault() || checklistItemId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _checklistItemDataProvider.ToggleChecklistItemExpandedAsync(User.GetUserId(), checklistId, checklistItemId, expanded);
            return Ok();
        }

        [HttpPost("move")]
        [ValidateAntiForgeryToken]
        [Permission(Permissions_CI.MoveId, Permissions_CI.MoveRouteId, Permissions_CI.MoveDescription)]
        public async Task<IActionResult> Move(Guid checklistId, Guid checklistItemId, string direction)
        {
            if (checklistId.IsDefault() || checklistItemId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _checklistItemDataProvider.MoveAsync(User.GetUserId(), checklistId, checklistItemId, direction);
            return Ok();
        }
    }
}