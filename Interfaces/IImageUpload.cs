namespace Landlords.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using System;

    public interface IImageUpload
    {
        Task<IActionResult> Upload(ICollection<IFormFile> files, Guid propertyId);
    }
}