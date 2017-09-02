namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using ViewModels;

    [Route("api/[controller]")]
    public class MaintenanceRequestsController : Controller
    {
        private readonly IMaintenanceRequestsDataProvider _maintenanceRequestsDataProvider;

        public MaintenanceRequestsController(IMaintenanceRequestsDataProvider maintenanceRequestsDataProvider)
        {
            _maintenanceRequestsDataProvider = maintenanceRequestsDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_MR.OverviewId, Permissions_MR.OverviewRouteId, Permissions_MR.OverviewDescription)]
        public async Task<IActionResult> Get()
        {
            if (User.IsTenant())
            {
                return Ok(await _maintenanceRequestsDataProvider.GetMaintenanceRequestsForTenant(User.GetUserId()));
            }

            return Ok(await _maintenanceRequestsDataProvider.GetMaintenanceRequests(User.GetPortfolioId()));
        }

        [HttpGet("ViewData")]
        [Permission(Permissions_MR.OverviewId, Permissions_MR.OverviewRouteId, Permissions_MR.OverviewDescription)]
        public IActionResult GetViewData()
        {
            return Ok(new MaintenanceRequestOverviewViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_MR.NewId, Permissions_MR.NewRouteId, Permissions_MR.NewDescription)]
        public async Task<IActionResult> New([FromBody] MaintenanceRequestViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (User.IsTenant())
                {
                    return Ok(await _maintenanceRequestsDataProvider.CreateForTenantAsync(User.GetUserId(), value));
                }
                return Ok(await _maintenanceRequestsDataProvider.CreateForPortfolioAsync(User.GetUserId(), User.GetPortfolioId(), value));
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}