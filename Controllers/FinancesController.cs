namespace Landlords.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Landlords.Permissions;
    using Model;
    using ViewModels;

    [Route("api/[controller]")]
    public class FinancesController : Controller
    {
        private readonly IFinancesDataProvider _financesDataProvider;

        public FinancesController(IFinancesDataProvider financesDataProvider)
        {
            _financesDataProvider = financesDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_FI.OverviewId, Permissions_FI.OverviewRouteId, Permissions_FI.OverviewDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _financesDataProvider.GetAccountsOverviewAsync(User.GetPortfolioId()));
        }

        [HttpGet("{accountId}")]
        [Permission(Permissions_FI.GetById, Permissions_FI.GetByRouteId, Permissions_FI.GetByDescription)]
        public async Task<IActionResult> Get(Guid accountId)
        {
            return Ok(await _financesDataProvider.GetAccountByIdAsync(User.GetPortfolioId(), accountId));
        }

        [HttpPost("new"), ValidateAntiForgeryToken]
        [Permission(Permissions_FI.NewId, Permissions_FI.NewRouteId, Permissions_FI.NewDescription)]
        public async Task<IActionResult> New()
        {
            return Ok(await _financesDataProvider.NewAsync(User.GetPortfolioId()));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_FI.UpdateId, Permissions_FI.UpdateRouteId, Permissions_FI.UpdateDescription)]
        public async Task<IActionResult> Post([FromBody] AccountViewModel value)
        {
            if (ModelState.IsValid)
            {
                await _financesDataProvider.UpdateAsync(User.GetPortfolioId(), value);
                return Ok();
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}