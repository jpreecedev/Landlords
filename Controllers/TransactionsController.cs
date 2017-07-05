namespace Landlords.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using System.Collections.Generic;
    using Database;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsDataProvider _transactionsDataProvider;
        private readonly ILLDbContext _context;

        public TransactionsController(ITransactionsDataProvider transactionsDataProvider, ILLDbContext context)
        {
            _transactionsDataProvider = transactionsDataProvider;
            _context = context;
        }

        [HttpGet]
        [Permission(Permissions_TR.GetById, Permissions_TR.GetByRouteId, Permissions_TR.GetByDescription)]
        public async Task<IActionResult> Get(Guid accountId)
        {
            return Ok(await _transactionsDataProvider.GetTransactionsAsync(User.GetPortfolioId(), accountId));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_TR.UpdateCategoryId, Permissions_TR.UpdateCategoryRouteId, Permissions_TR.UpdateCategoryDescription)]
        public async Task<IActionResult> Post(Guid accountId, Guid transactionId, string category)
        {
            await _transactionsDataProvider.UpdateTransactionCategoryAsync(User.GetPortfolioId(), accountId, transactionId, category);
            return Ok();
        }

        [HttpPost("upload"), ValidateAntiForgeryToken, MustOwnAccount]
        [Permission(Permissions_TR.UploadId, Permissions_TR.UploadRouteId, Permissions_TR.UploadDescription)]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, Guid entityId)
        {
            try
            {
                var account = await _context.Accounts.FirstAsync(c => c.PortfolioId == User.GetPortfolioId() && c.Id == entityId);
                var transactions = account.GetStatementProcessor().Process(files, entityId);

                return Ok(await _transactionsDataProvider.ProcessImportedTransactionsAsync(transactions));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}