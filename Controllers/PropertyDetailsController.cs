namespace Landlords.Controllers
{
    using DataProviders;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using System;
    using Landlords.Core;
    using Landlords.ViewModels;

    [Route("api/[controller]")]
    public class PropertyDetailsController : Controller
    {
        private readonly IPropertyDataProvider _propertyDataProvider;

        public PropertyDetailsController(IPropertyDataProvider dataAccessProvider)
        {
            _propertyDataProvider = dataAccessProvider;
        }

        [HttpGet]
        public PropertyDetails Get(Guid propertyId)
        {
            return _propertyDataProvider.GetDetails(User.GetUserId(), propertyId);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PropertyDetailsViewModel value)
        {
            if (ModelState.IsValid)
            {
                _propertyDataProvider.Create(User.GetUserId(), value);
                return Ok();
            }
            return BadRequest();
        }
    }
}