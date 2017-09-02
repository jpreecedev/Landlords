namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;

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
    }
}