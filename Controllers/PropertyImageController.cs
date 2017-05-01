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
    using Model;

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

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid entityId)
        {
            try
            {
                var userId = User.GetUserId();

                if (entityId.IsDefault() || !userId.Owns<PropertyImage>(entityId, _context))
                    return BadRequest("Unable to validate payload");

                await _dataProvider.DeleteAsync(userId, entityId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("upload"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, Guid entityId)
        {
            try
            {
                var userId = User.GetUserId();

                if (!files.IsValid() || entityId.IsDefault() || !userId.Owns<PropertyDetails>(entityId, _context))
                    return BadRequest("Unable to validate payload");

                return Ok(await _dataProvider.UploadAsync(files, userId, entityId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}