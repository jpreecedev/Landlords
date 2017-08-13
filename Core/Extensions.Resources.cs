namespace Landlords.Core
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using ViewModels;
    using System.IO;
    using System.Threading.Tasks;

    public static class ResourceExtensions
    {
        public static async Task<List<ResourceViewModel>> ProcessResourcesAsync(this ICollection<IFormFile> files, string webRootPath, string subDirectoryPath)
        {
            if (files == null)
            {
                return null;
            }

            var uploadDirectory = Path.Combine(webRootPath, "static", "uploads", subDirectoryPath);
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var result = new List<ResourceViewModel>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fullPath = Path.Combine(uploadDirectory, file.FileName);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        result.Add(new ResourceViewModel
                        {
                            FileName = file.FileName,
                            FilePath = fullPath
                        });
                    }
                }
            }

            return result;
        }
    }
}