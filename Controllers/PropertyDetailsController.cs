namespace Landlords.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Landlords.Core;
    using Landlords.ViewModels;
    using System.Threading.Tasks;
    using Database;
    using Landlords.Permissions;
    using Landlords.Interfaces;
    using Model.Permissions;

    [Route("api/[controller]")]
    public class PropertyDetailsController : Controller
    {
        private readonly IPropertyDataProvider _propertyDataProvider;
        private readonly ILLDbContext _context;

        public PropertyDetailsController(IPropertyDataProvider dataAccessProvider, ILLDbContext context)
        {
            _propertyDataProvider = dataAccessProvider;
            _context = context;
        }

        [HttpGet("ViewData")]
        [Permission(PropertyDetails.ViewId, PropertyDetails.ViewRouteId, PropertyDetails.ViewDescription)]
        public IActionResult GetViewData()
        {
            return Ok(new PropertyDetailsViewModel());
        }

        [HttpPost("new"), ValidateAntiForgeryToken, MustOwnPortfolio]
        [Permission(PropertyDetails.NewId, PropertyDetails.NewRouteId, PropertyDetails.NewDescription)]
        public async Task<IActionResult> New()
        {
            return Ok(await _propertyDataProvider.NewAsync(User.GetPortfolioId()));
        }

        [HttpGet, MustOwnPortfolio]
        [Permission(PropertyDetails.GetListId, PropertyDetails.GetListRouteId, PropertyDetails.GetListDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _propertyDataProvider.GetListAsync(User.GetPortfolioId()));
        }

        [HttpGet("{propertyId}"), MustOwnPropertyDetails]
        [Permission(PropertyDetails.GetById, PropertyDetails.GetByRouteId, PropertyDetails.GetByIdDescription)]
        public async Task<IActionResult> Get(Guid propertyId)
        {
            return Ok(await _propertyDataProvider.GetDetailsAsync(propertyId));
        }

        [HttpPost, ValidateAntiForgeryToken, MustOwnPropertyDetails]
        [Permission(PropertyDetails.UpdateId, PropertyDetails.UpdateRouteId, PropertyDetails.UpdateDescription)]
        public async Task<IActionResult> Post([FromBody] PropertyDetailsViewModel value)
        {
            if (ModelState.IsValid)
            {
                await _propertyDataProvider.UpdateAsync(value);
                return Ok();
            }
            
            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}