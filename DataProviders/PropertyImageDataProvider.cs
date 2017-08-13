namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Landlords.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Landlords.Interfaces;
    using Model.Entities;
    using Landlords.Core;

    public class PropertyImageDataProvider : BaseDataProvider, IPropertyImageDataProvider
    {
        public PropertyImageDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task DeleteAsync(Guid propertyImageId)
        {
            var entity = await Context.PropertyImages.FirstOrDefaultAsync(c => c.Id == propertyImageId);
            if (entity != null)
            {
                entity.Deleted = DateTime.Now;
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<PropertyImageViewModel>> UploadAsync(ICollection<IFormFile> files, Guid portfolioId, Guid propertyId)
        {
            var result = new List<PropertyImageViewModel>();

            var resources = await files.ProcessResourcesAsync(HostingEnvironment.WebRootPath, portfolioId.ToString());
            if (resources == null)
            {
                return null;
            }

            foreach (var resource in resources)
            {
                var entity = new PropertyImage
                {
                    PropertyId = propertyId,
                    FileName = resource.FileName,
                    Created = DateTime.Now
                };

                await Context.PropertyImages.AddAsync(entity);
                await Context.SaveChangesAsync();

                result.Add(new PropertyImageViewModel(entity));
            }

            return result;
        }
    }
}