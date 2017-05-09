namespace Landlords.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Landlords.Permissions;
    using PropertyImage = Model.Permissions.PropertyImage;

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

        [HttpDelete, MustOwnPropertyImage]
        [Permission(PropertyImage.DeleteId, PropertyImage.DeleteRouteId, PropertyImage.DeleteDescription)]
        public async Task<IActionResult> Delete(Guid entityId)
        {
            try
            {
                await _dataProvider.DeleteAsync(entityId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("upload"), ValidateAntiForgeryToken, MustOwnPropertyDetails]
        [Permission(PropertyImage.UploadId, PropertyImage.UploadRouteId, PropertyImage.UploadDescription)]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, Guid entityId)
        {
            try
            {
                return Ok(await _dataProvider.UploadAsync(files, User.GetPortfolioId(), entityId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}