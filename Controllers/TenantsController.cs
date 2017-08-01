namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using System;
    using ViewModels;

    [Route("api/[controller]")]
    public class TenantsController : Controller
    {
        private readonly ITenantsDataProvider _tenantsDataProvider;

        public TenantsController(ITenantsDataProvider tenantsDataProvider)
        {
            _tenantsDataProvider = tenantsDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_TE.GetListById, Permissions_TE.GetListByRouteId, Permissions_TE.GetListByDescription)]
        public async Task<IActionResult> Get()
        {
            var portfolioId = User.GetPortfolioId();
            var agencyId = User.GetAgencyId();

            if (agencyId != default(Guid))
            {
                return Ok(await _tenantsDataProvider.GetTenantsByAgencyIdAsync(agencyId));
            }

            return Ok(await _tenantsDataProvider.GetTenantsByPortfolioIdAsync(portfolioId));
        }

        [HttpGet("{tenantId}")]
        [Permission(Permissions_TE.GetById, Permissions_TE.GetByRouteId, Permissions_TE.GetByDescription)]
        public async Task<IActionResult> Get(Guid tenantId)
        {
            return Ok(await _tenantsDataProvider.GetTenantByIdAsync(tenantId));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_TE.UpdateId, Permissions_TE.UpdateRouteId, Permissions_TE.UpdateDescription)]
        public async Task<IActionResult> Post([FromBody] TenantViewModel value)
        {
            if (ModelState.IsValid)
            {
                await _tenantsDataProvider.UpdateAsync(value);
                return Ok();
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}