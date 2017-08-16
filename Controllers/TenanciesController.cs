namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using System;

    [Route("api/[controller]")]
    public class TenanciesController : Controller
    {
        private readonly ITenanciesDataProvider _tenanciesDataProvider;

        public TenanciesController(ITenanciesDataProvider tenanciesDataProvider)
        {
            _tenanciesDataProvider = tenanciesDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_TT.GetListById, Permissions_TT.GetListByRouteId, Permissions_TT.GetListByDescription)]
        public async Task<IActionResult> Get()
        {
            var portfolioId = User.GetPortfolioId();
            var agencyId = User.GetAgencyId();

            if (agencyId != default(Guid))
            {
                return Ok(await _tenanciesDataProvider.GetTenanciesByAgencyIdAsync(agencyId));
            }

            return Ok(await _tenanciesDataProvider.GetTenanciesByPortfolioIdAsync(portfolioId));
        }

        [HttpGet("tenancy")]
        [Permission(Permissions_TT.GetById, Permissions_TT.GetByRouteId, Permissions_TT.GetByDescription)]
        public async Task<IActionResult> Get(Guid tenancyId)
        {
            return Ok(await _tenanciesDataProvider.GetTenancyJourneyByIdAsync(User.GetPortfolioId(), tenancyId));
        }
    }
}