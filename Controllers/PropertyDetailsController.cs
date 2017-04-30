namespace Landlords.Controllers
{
    using DataProviders;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using Landlords.Core;
    using Landlords.ViewModels;
    using System.Threading.Tasks;
    using Model;

    [Route("api/[controller]")]
    public class PropertyDetailsController : Controller
    {
        private readonly IPropertyDataProvider _propertyDataProvider;

        public PropertyDetailsController(IPropertyDataProvider dataAccessProvider)
        {
            _propertyDataProvider = dataAccessProvider;
        }

        [HttpGet("ViewData")]
        public PropertyDetailsViewModel GetViewData()
        {
            return new PropertyDetailsViewModel();
        }

        [HttpGet]
        public async Task<PropertyDetailsViewModel[]> Get()
        {
            var properties = await _propertyDataProvider.GetListAsync(User.GetUserId());
            return properties.ToArray();
        }

        [HttpGet("{propertyId}")]
        public async Task<PropertyDetails> Get(Guid propertyId)
        {
            return await _propertyDataProvider.GetDetailsAsync(User.GetUserId(), propertyId);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PropertyDetailsViewModel value)
        {
            if (ModelState.IsValid)
            {
                await _propertyDataProvider.CreateAsync(User, value);
                return Ok();
            }
            
            return BadRequest(new { Errors = ModelState.Errors() });
        }
    }
}