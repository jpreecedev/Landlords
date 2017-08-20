namespace Landlords.Controllers
{
    using Landlords.Core;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using System.Threading.Tasks;
    using Interfaces;
    using ViewModels;

    [Route("api/[controller]")]
    public class JourneysController : Controller
    {
        private readonly IJourneyDataProvider _journeyDataProvider;

        public JourneysController(IJourneyDataProvider journeyDataProvider)
        {
            _journeyDataProvider = journeyDataProvider;
        }

        [HttpGet("StartTenancy")]
        [Permission(Permissions_J.StartTenancyId, Permissions_J.StartTenancyRouteId, Permissions_J.StartTenancyDescription)]
        public IActionResult GetViewData()
        {
            return Ok(new StartTenancyJourneyViewModel());
        }

        [HttpPost("StartTenancy"), ValidateAntiForgeryToken]
        [Permission(Permissions_J.CreateTenancyId, Permissions_J.CreateTenancyRouteId, Permissions_J.CreateTenancyDescription)]
        public async Task<IActionResult> Post([FromBody] StartTenancyJourneyViewModel value)
        {
            var portfolioId = User.GetPortfolioId();
            if (ModelState.IsValid)
            {
                if (value.IsNew())
                {
                    await _journeyDataProvider.CreateTenancyAsync(portfolioId, value);
                }
                else
                {
                    await _journeyDataProvider.UpdateTenancyAsync(portfolioId, value);
                }

                return Ok();
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}