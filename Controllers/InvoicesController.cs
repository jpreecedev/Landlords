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

    [Route("api/finances/[controller]")]
    public class InvoicesController : Controller
    {
        private readonly IInvoicesDataProvider _invoicesDataProvider;

        public InvoicesController(IInvoicesDataProvider invoicesDataProvider)
        {
            _invoicesDataProvider = invoicesDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_IN.GetId, Permissions_IN.GetRouteId, Permissions_IN.GetDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _invoicesDataProvider.GetAsync(User.GetPortfolioId()));
        }

        [HttpGet("{invoiceId}")]
        [Permission(Permissions_IN.GetById, Permissions_IN.GetByRouteId, Permissions_IN.GetByDescription)]
        public async Task<IActionResult> Get(Guid invoiceId)
        {
            return Ok(await _invoicesDataProvider.GetByIdAsync(User.GetPortfolioId(), invoiceId));
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_IN.SaveInvoiceId, Permissions_IN.SaveInvoiceRouteId, Permissions_IN.SaveInvoiceDescription)]
        public async Task<IActionResult> Post([FromBody] InvoiceViewModel value)
        {
            if (ModelState.IsValid)
            {
                await _invoicesDataProvider.UpdateAsync(User.GetPortfolioId(), value);
                return Ok();
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }

        [HttpDelete, ValidateAntiForgeryToken, MustOwnInvoice]
        [Permission(Permissions_IN.DeleteInvoiceId, Permissions_IN.DeleteInvoiceRouteId, Permissions_IN.DeleteInvoiceDescription)]
        public async Task<IActionResult> Delete(Guid entityId)
        {
            if (entityId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _invoicesDataProvider.DeleteAsync(User.GetPortfolioId(), entityId);
            return Ok();
        }
    }
}