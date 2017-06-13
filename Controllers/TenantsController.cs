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
            return Ok(await _tenantsDataProvider.GetTenantsAsync(User.GetPortfolioId()));
        }

        [HttpGet("{tenantId}")]
        [Permission(Permissions_TE.GetById, Permissions_TE.GetByRouteId, Permissions_TE.GetByDescription)]
        public async Task<IActionResult> Get(Guid tenantId)
        {
            return Ok(await _tenantsDataProvider.GetTenantByIdAsync(User.GetPortfolioId(), tenantId));
        }

        [HttpPost("new"), ValidateAntiForgeryToken]
        [Permission(Permissions_TE.NewId, Permissions_TE.NewRouteId, Permissions_TE.NewDescription)]
        public async Task<IActionResult> New()
        {
            return Ok(await _tenantsDataProvider.NewAsync(User.GetPortfolioId()));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_TE.UpdateId, Permissions_TE.UpdateRouteId, Permissions_TE.UpdateDescription)]
        public async Task<IActionResult> Post([FromBody] TenantViewModel value)
        {
            if (ModelState.IsValid)
            {
                await _tenantsDataProvider.UpdateAsync(User.GetPortfolioId(), value);
                return Ok();
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}