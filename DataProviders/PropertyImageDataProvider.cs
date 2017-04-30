namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Model;

    public class PropertyImageDataProvider : BaseDataProvider<PropertyImage>, IPropertyImageDataProvider
    {
        private readonly LLDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PropertyImageDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public async Task<List<string>> UploadAsync(ICollection<IFormFile> files, Guid userId, Guid propertyId)
        {
            var uploadDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "static", "uploads", userId.ToString());
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
                        await AttachToPropertyAsync(userId, propertyId, file.FileName);
                        result.Add(new Uri(Path.Combine("/static/uploads", userId.ToString(), file.FileName), UriKind.Relative).ToString().Replace("\\", "/"));
                    }
                }
            }

            return result;
        }

        private async Task AttachToPropertyAsync(Guid userId, Guid propertyId, string fileName)
        {
            var entity = new PropertyImage
            {
                UserId = userId,
                PropertyId = propertyId,
                FileName = fileName
            };

            PopulateNewEntity(userId, entity);

            await _context.PropertyImages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}