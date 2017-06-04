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
    using Model;

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
        [Permission(Permissions_PD.ViewId, Permissions_PD.ViewRouteId, Permissions_PD.ViewDescription)]
        public IActionResult GetViewData()
        {
            return Ok(new PropertyDetailsViewModel());
        }

        [HttpPost("new"), ValidateAntiForgeryToken, MustOwnPortfolio]
        [Permission(Permissions_PD.NewId, Permissions_PD.NewRouteId, Permissions_PD.NewDescription)]
        public async Task<IActionResult> New()
        {
            return Ok(await _propertyDataProvider.NewAsync(User.GetPortfolioId()));
        }

        [HttpGet, MustOwnPortfolio]
        [Permission(Permissions_PD.GetListId, Permissions_PD.GetListRouteId, Permissions_PD.GetListDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _propertyDataProvider.GetListAsync(User.GetPortfolioId()));
        }

        [HttpGet("{propertyId}"), MustOwnPropertyDetails]
        [Permission(Permissions_PD.GetById, Permissions_PD.GetByRouteId, Permissions_PD.GetByIdDescription)]
        public async Task<IActionResult> Get(Guid propertyId)
        {
            return Ok(await _propertyDataProvider.GetDetailsAsync(propertyId));
        }

        [HttpGet("BasicDetails")]
        [Permission(Permissions_PD.GetBasicId, Permissions_PD.GetBasicRouteId, Permissions_PD.GetBasicIdDescription)]
        public async Task<IActionResult> GetBasicDetails()
        {
            return Ok(await _propertyDataProvider.GetBasicDetailsAsync(User.GetUserId()));
        }

        [HttpPost, ValidateAntiForgeryToken, MustOwnPropertyDetails]
        [Permission(Permissions_PD.UpdateId, Permissions_PD.UpdateRouteId, Permissions_PD.UpdateDescription)]
        public async Task<IActionResult> Post([FromBody] PropertyDetailsViewModel value)
        {
            if (ModelState.IsValid)
            {
                var portfolioId = User.GetPortfolioId();
                await _propertyDataProvider.UpdateAsync(portfolioId, value);
                return Ok();
            }
            
            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}