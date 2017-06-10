namespace Landlords.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;

    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsDataProvider _transactionsDataProvider;

        public TransactionsController(ITransactionsDataProvider transactionsDataProvider)
        {
            _transactionsDataProvider = transactionsDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_TR.GetById, Permissions_TR.GetByRouteId, Permissions_TR.GetByDescription)]
        public async Task<IActionResult> Get(Guid accountId)
        {
            return Ok(await _transactionsDataProvider.GetTransactionsAsync(User.GetPortfolioId(), accountId));
        }
    }
}