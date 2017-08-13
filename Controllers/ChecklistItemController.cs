namespace Landlords.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    [Route("api/[controller]")]
    public class ChecklistItemController : Controller
    {
        private readonly IChecklistItemDataProvider _checklistItemDataProvider;

        public ChecklistItemController(IChecklistItemDataProvider checklistItemDataProvider)
        {
            _checklistItemDataProvider = checklistItemDataProvider;
        }

        [HttpPost("completed"), ValidateAntiForgeryToken, MustOwnChecklist]
        [Permission(Permissions_CI.ToggleCompletedId, Permissions_CI.ToggleCompletedRouteId, Permissions_CI.ToggleCompletedDescription)]
        public async Task<IActionResult> Completed(Guid checklistId, Guid checklistItemId, bool completed)
        {
            if (checklistId.IsDefault() || checklistItemId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _checklistItemDataProvider.ToggleChecklistItemCompletedAsync(User.GetPortfolioId(), checklistId, checklistItemId, completed);
            return Ok();
        }

        [HttpPost("move"), ValidateAntiForgeryToken, MustOwnChecklist]
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

        [HttpPost("template"), ValidateAntiForgeryToken, MustOwnChecklist]
        [Permission(Permissions_CI.ApplyTemplateId, Permissions_CI.ApplyTemplateRouteId, Permissions_CI.ApplyTemplateDescription)]
        public async Task<IActionResult> ApplyTemplate(Guid checklistId, Guid checklistItemId, [FromBody] object payload)
        {
            if (checklistId.IsDefault() || checklistItemId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _checklistItemDataProvider.ApplyTemplatePayloadAsync(User.GetPortfolioId(), checklistId, checklistItemId, payload?.ToString());
            return Ok();
        }

        [HttpPost("upload"), ValidateAntiForgeryToken, MustOwnChecklist]
        [Permission(Permissions_CI.UploadDocumentId, Permissions_CI.UploadDocumentRouteId, Permissions_CI.UploadDocumentDescription)]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, Guid checklistId, Guid checklistItemId)
        {
            if (checklistId.IsDefault() || checklistItemId.IsDefault())

            {
                return BadRequest("Unable to validate payload");
            }

            try
            {
                return Ok(await _checklistItemDataProvider.UploadAsync(files, checklistId, checklistItemId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}