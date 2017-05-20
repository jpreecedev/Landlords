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
            
            return Ok(await _checklistDataProvider.GetChecklistOverviewAsync(userId, agencyId));
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
    }
}