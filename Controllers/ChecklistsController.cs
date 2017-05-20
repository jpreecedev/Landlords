namespace Landlords.Controllers
{
    using Landlords.Permissions;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using System.Threading.Tasks;
    using Core;
    using Interfaces;

    [Route("api/[controller]")]
    public class ChecklistsController : Controller
    {
        private readonly IChecklistDataProvider _checklistDataProvider;

        public ChecklistsController(IChecklistDataProvider checklistDataProvider)
        {
            this._checklistDataProvider = checklistDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_CL.OverviewId, Permissions_CL.OverviewRouteId, Permissions_CL.OverviewDescription)]
        public async Task<IActionResult> Get()
        {
            var userId = User.GetUserId();
            var agencyId = User.GetAgencyId();
            
            return Ok(await _checklistDataProvider.GetChecklistOverviewAsync(userId, agencyId));
        }
    }
}