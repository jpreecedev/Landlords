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
    using Landlords.ViewModels;

    public class PropertyImageDataProvider : BaseDataProvider<PropertyImage>, IPropertyImageDataProvider
    {
        public PropertyImageDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ICollection<PropertyImageViewModel>> UploadAsync(ICollection<IFormFile> files, Guid userId, Guid propertyId)
        {
            var uploadDirectory = Path.Combine(HostingEnvironment.WebRootPath, "static", "uploads", userId.ToString());
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var result = new List<PropertyImageViewModel>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploadDirectory, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);

                        var entity = await AttachToPropertyAsync(userId, propertyId, file.FileName);
                        result.Add(new PropertyImageViewModel(entity));
                    }
                }
            }

            return result;
        }

        private async Task<PropertyImage> AttachToPropertyAsync(Guid userId, Guid propertyId, string fileName)
        {
            var entity = new PropertyImage
            {
                UserId = userId,
                PropertyId = propertyId,
                FileName = fileName
            };

            PopulateNewEntity(userId, entity);

            await Context.PropertyImages.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity;
        }
    }
}