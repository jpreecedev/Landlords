namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using ViewModels;

    public interface IPropertyImageDataProvider
    {
        Task<ICollection<PropertyImageViewModel>> UploadAsync(ICollection<IFormFile> files, Guid userId, Guid propertyId);
        Task DeleteAsync(Guid propertyImageId);
    }
}