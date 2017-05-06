namespace Landlords.Controllers
{
    using DataProviders;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Landlords.Core;
    using Landlords.ViewModels;
    using System.Threading.Tasks;
    using Model;
    using Database;
    using Landlords.Permissions;

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
        [RequiresPermission(Permissions.PropertyDetailsViewData)]
        public IActionResult GetViewData()
        {
            return Ok(new PropertyDetailsViewModel());
        }

        [HttpPost("new"), ValidateAntiForgeryToken]
        [MustOwnPortfolio, RequiresPermission(Permissions.PropertyDetailsNew)]
        public async Task<IActionResult> New()
        {
            return Ok(await _propertyDataProvider.NewAsync(User.GetPortfolioId()));
        }

        [HttpGet]
        [MustOwnPortfolio, RequiresPermission(Permissions.PropertyDetailsGetList)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _propertyDataProvider.GetListAsync(User.GetPortfolioId()));
        }

        [HttpGet("{propertyId}")]
        [MustOwnPropertyDetails, RequiresPermission(Permissions.PropertyDetailsGetById)]
        public async Task<IActionResult> Get(Guid propertyId)
        {
            return Ok(await _propertyDataProvider.GetDetailsAsync(propertyId));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [MustOwnPropertyDetails, RequiresPermission(Permissions.PropertyDetailsUpdate)]
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