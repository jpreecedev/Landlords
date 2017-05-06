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
        [RequiresPermission(Permissions.PropertyDetailsNew)]
        public async Task<IActionResult> New()
        {
            return Ok(await _propertyDataProvider.NewAsync(User.GetUserId()));
        }

        [HttpGet]
        [RequiresPermission(Permissions.PropertyDetailsGetList)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _propertyDataProvider.GetListAsync(User.GetUserId()));
        }

        [HttpGet("{propertyId}")]
        [RequiresPermission(Permissions.PropertyDetailsGetById)]
        public async Task<IActionResult> Get(Guid propertyId)
        {
            var userId = User.GetUserId();

            if (propertyId.IsDefault() || !userId.Owns<PropertyDetails>(propertyId, _context))
                return BadRequest("Unable to validate payload");

            return Ok(await _propertyDataProvider.GetDetailsAsync(User.GetUserId(), propertyId));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [RequiresPermission(Permissions.PropertyDetailsUpdate)]
        public async Task<IActionResult> Post([FromBody] PropertyDetailsViewModel value)
        {
            if (ModelState.IsValid)
            {
                var userId = User.GetUserId();

                if (value.Id.IsDefault() || !userId.Owns<PropertyDetails>(value.Id, _context))
                    return BadRequest("Unable to validate payload");
                
                await _propertyDataProvider.UpdateAsync(User.GetUserId(), value);
                return Ok();
            }
            
            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}