namespace Landlords.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using DataProviders;
    using Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class PropertyImageController : Controller, IImageUpload
    {
        private readonly IPropertyImageDataProvider _dataProvider;
        private readonly ILLDbContext _context;

        public PropertyImageController(IPropertyImageDataProvider dataProvider, ILLDbContext context)
        {
            _dataProvider = dataProvider;
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, Guid propertyId)
        {
            try
            {
                var userId = User.GetUserId();

                if (!files.IsValid() || propertyId.IsDefault() || !userId.Owns(propertyId, _context))
                    return BadRequest("Unable to validate payload");

                return Ok(await _dataProvider.UploadAsync(files, userId, propertyId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}