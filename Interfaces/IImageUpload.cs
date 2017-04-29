namespace Landlords.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;

    public interface IImageUpload
    {
        Task<IActionResult> Upload(ICollection<IFormFile> files);
    }
}