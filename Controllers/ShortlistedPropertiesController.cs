namespace Landlords.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using ViewModels;

    [Route("api/[controller]")]
    public class ShortlistedPropertiesController : Controller
    {
        private readonly IShortlistedPropertiesDataProvider _shortlistedPropertiesDataProvider;
        private readonly IPropertyDataProvider _propertyDataProvider;

        public ShortlistedPropertiesController(IShortlistedPropertiesDataProvider shortlistedPropertiesDataProvider, IPropertyDataProvider propertyDataProvider)
        {
            _shortlistedPropertiesDataProvider = shortlistedPropertiesDataProvider;
            _propertyDataProvider = propertyDataProvider;
        }

        [HttpGet]
        [Permission(Permissions_SP.GetListById, Permissions_SP.GetListByRouteId, Permissions_SP.GetListByDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _shortlistedPropertiesDataProvider.GetListAsync(User.GetPortfolioId()));
        }

        [HttpGet("{shortlistedPropertyId}")]
        [Permission(Permissions_SP.GetById, Permissions_SP.GetByRouteId, Permissions_SP.GetByDescription)]
        public async Task<IActionResult> Get(Guid shortlistedPropertyId)
        {
            return Ok(await _shortlistedPropertiesDataProvider.GetAsync(User.GetPortfolioId(), shortlistedPropertyId));
        }

        [HttpDelete, ValidateAntiForgeryToken]
        [Permission(Permissions_SP.DeleteId, Permissions_SP.DeleteRouteId, Permissions_SP.DeleteDescription)]
        public async Task<IActionResult> Delete(Guid shortlistedPropertyId)
        {
            if (shortlistedPropertyId.IsDefault())
            {
                return BadRequest("Unable to validate payload");
            }

            await _shortlistedPropertiesDataProvider.DeleteAsync(User.GetPortfolioId(), shortlistedPropertyId);
            return Ok();
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_SP.UpdateId, Permissions_SP.UpdateRouteId, Permissions_SP.UpdateDescription)]
        public async Task<IActionResult> Post([FromBody] ShortlistedPropertyViewModel value)
        {
            if (ModelState.IsValid)
            {
                await _shortlistedPropertiesDataProvider.UpdateAsync(User.GetPortfolioId(), value);
                return Ok();
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }

        [HttpPost("promote"), ValidateAntiForgeryToken]
        [Permission(Permissions_SP.PromoteId, Permissions_SP.PromoteRouteId, Permissions_SP.PromoteDescription)]
        public async Task<IActionResult> Promote([FromBody] ShortlistedPropertyViewModel value)
        {
            if (ModelState.IsValid)
            {
                var propertyDetailsId = await _propertyDataProvider.PromoteAsync(User.GetPortfolioId(), value);
                return Ok(propertyDetailsId);
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}