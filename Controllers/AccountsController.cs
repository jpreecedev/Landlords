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
    public class AccountsController : Controller
    {
        private readonly IAccountsDataProvider _accountsDataProvider;

        public AccountsController(IAccountsDataProvider accountsDataProvider)
        {
            _accountsDataProvider = accountsDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_AC.OverviewId, Permissions_AC.OverviewRouteId, Permissions_AC.OverviewDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _accountsDataProvider.GetAccountsOverviewAsync(User.GetPortfolioId()));
        }

        [HttpGet("{accountId}")]
        [Permission(Permissions_AC.GetById, Permissions_AC.GetByRouteId, Permissions_AC.GetByDescription)]
        public async Task<IActionResult> Get(Guid accountId)
        {
            return Ok(await _accountsDataProvider.GetAccountByIdAsync(User.GetPortfolioId(), accountId));
        }

        [HttpPost("new"), ValidateAntiForgeryToken]
        [Permission(Permissions_AC.NewId, Permissions_AC.NewRouteId, Permissions_AC.NewDescription)]
        public async Task<IActionResult> New()
        {
            return Ok(await _accountsDataProvider.NewAsync(User.GetPortfolioId()));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_AC.UpdateId, Permissions_AC.UpdateRouteId, Permissions_AC.UpdateDescription)]
        public async Task<IActionResult> Post([FromBody] AccountViewModel value)
        {
            if (ModelState.IsValid)
            {
                await _accountsDataProvider.UpdateAsync(User.GetPortfolioId(), value);
                return Ok();
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}