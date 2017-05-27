namespace Landlords.Controllers
{
    using Landlords.Permissions;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using System;

    [Route("api/[controller]")]
    public class ChecklistsController : Controller
    {
        private readonly IChecklistDataProvider _checklistDataProvider;

        public ChecklistsController(IChecklistDataProvider checklistDataProvider)
        {
            _checklistDataProvider = checklistDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_CL.OverviewId, Permissions_CL.OverviewRouteId, Permissions_CL.OverviewDescription)]
        public async Task<IActionResult> Get()
        {
            var userId = User.GetUserId();
            var agencyId = User.GetAgencyId();
            
            return Ok(await _checklistDataProvider.GetChecklistOverviewAsync(userId, agencyId, false));
        }

        [HttpGet("{checklistId}")]
        [Permission(Permissions_CL.GetById, Permissions_CL.GetByRouteId, Permissions_CL.GetByDescription)]
        public async Task<IActionResult> Get(Guid checklistId)
        {
            return Ok(await _checklistDataProvider.GetChecklistByIdAsync(User.GetUserId(), checklistId));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_CL.CreateId, Permissions_CL.CreateRouteId, Permissions_CL.CreateDescription)]
        public async Task<IActionResult> Post(Guid checklistId)
        {
            if (checklistId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            return Ok(await _checklistDataProvider.CreateChecklistInstanceAsync(User.GetUserId(), checklistId));
        }

        [HttpPost("archive"), ValidateAntiForgeryToken]
        [Permission(Permissions_CL.ArchiveId, Permissions_CL.ArchiveRouteId, Permissions_CL.ArchiveDescription)]
        public async Task<IActionResult> Archive(Guid checklistId)
        {
            if (checklistId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _checklistDataProvider.ArchiveChecklistInstanceAsync(User.GetUserId(), checklistId);
            return Ok();
        }

        [HttpGet("archived")]
        [Permission(Permissions_CL.ArchivedId, Permissions_CL.ArchivedRouteId, Permissions_CL.ArchivedDescription)]
        public async Task<IActionResult> Archived()
        {
            return Ok(await _checklistDataProvider.GetArchivedChecklistInstancesAsync(User.GetUserId()));
        }

        [HttpDelete("{checklistId}"), ValidateAntiForgeryToken]
        [Permission(Permissions_CL.DeleteById, Permissions_CL.DeleteByRouteId, Permissions_CL.DeleteByDescription)]
        public async Task<IActionResult> Delete(Guid checklistId)
        {
            if (checklistId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _checklistDataProvider.DeleteInstanceAsync(User.GetUserId(), checklistId);
            return Ok();
        }
    }
}