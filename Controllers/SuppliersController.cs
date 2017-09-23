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
    public class SuppliersController : Controller
    {
        private readonly ISuppliersDataProvider _suppliersDataProvider;

        public SuppliersController(ISuppliersDataProvider suppliersDataProvider)
        {
            _suppliersDataProvider = suppliersDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_SU.GetId, Permissions_SU.GetRouteId, Permissions_SU.GetDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _suppliersDataProvider.GetAsync(User.GetPortfolioId()));
        }

        [HttpGet("{supplierId}")]
        [Permission(Permissions_SU.GetById, Permissions_SU.GetByRouteId, Permissions_SU.GetByDescription)]
        public async Task<IActionResult> Get(Guid supplierId)
        {
            return Ok(await _suppliersDataProvider.GetByIdAsync(User.GetPortfolioId(), supplierId));
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_SU.SaveSupplierId, Permissions_SU.SaveSupplierRouteId, Permissions_SU.SaveSupplierDescription)]
        public async Task<IActionResult> Post([FromBody] SupplierViewModel value)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _suppliersDataProvider.UpdateAsync(User.GetPortfolioId(), value));
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }

        [HttpDelete, ValidateAntiForgeryToken, MustOwnSupplier]
        [Permission(Permissions_SU.DeleteSupplierId, Permissions_SU.DeleteSupplierRouteId, Permissions_SU.DeleteSupplierDescription)]
        public async Task<IActionResult> Delete(Guid entityId)
        {
            if (entityId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _suppliersDataProvider.DeleteAsync(User.GetPortfolioId(), entityId);
            return Ok();
        }
    }
}