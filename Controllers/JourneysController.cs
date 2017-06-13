namespace Landlords.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using ViewModels;

    [Route("api/[controller]")]
    public class JourneysController : Controller
    {
        [HttpGet("StartTenancy")]
        [Permission(Permissions_J.StartTenancyId, Permissions_J.StartTenancyRouteId, Permissions_J.StartTenancyDescription)]
        public IActionResult GetViewData()
        {
            return Ok(new StartTenancyWizardViewModel());
        }
    }
}