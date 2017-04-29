namespace Landlords.Controllers
{
    using DataProviders;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using Landlords.Core;
    using Landlords.ViewModels;
    using System.Threading.Tasks;
    using Landlords.Interfaces;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    [Route("api/[controller]")]
    public class PropertyDetailsController : Controller, IImageUpload
    {
        private readonly IPropertyDataProvider _propertyDataProvider;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PropertyDetailsController(IPropertyDataProvider dataAccessProvider, IHostingEnvironment hostingEnvironment)
        {
            _propertyDataProvider = dataAccessProvider;
            _hostingEnvironment = hostingEnvironment;
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
            return properties.Select(property => new PropertyDetailsViewModel(property)).ToArray();
        }

        [HttpGet("{propertyId}")]
        public async Task<PropertyDetailsViewModel> Get(Guid propertyId)
        {
            var propertyDetails = await _propertyDataProvider.GetDetailsAsync(User.GetUserId(), propertyId);
            return new PropertyDetailsViewModel(propertyDetails);
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

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            try
            {
                return Ok(await new ImageUploadHelper().ProcessUploadedFiles(_hostingEnvironment, files, User.GetUserId()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}