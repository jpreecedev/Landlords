namespace Landlords.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class ImageUploadHelper
    {
        public async Task<List<string>> ProcessUploadedFiles(IHostingEnvironment hostingEnvironment, ICollection<IFormFile> files, Guid userId)
        {
            var uploadDirectory = Path.Combine(hostingEnvironment.WebRootPath, "static", "uploads", userId.ToString());
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var result = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploadDirectory, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        result.Add(new Uri(Path.Combine("/static/uploads", userId.ToString(), file.FileName), UriKind.Relative).ToString().Replace("\\", "/"));
                    }
                }
            }

            return result;
        }
    }
}