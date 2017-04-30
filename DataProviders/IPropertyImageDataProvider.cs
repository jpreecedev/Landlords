using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Landlords.DataProviders
{
    public interface IPropertyImageDataProvider
    {
        Task<List<string>> UploadAsync(ICollection<IFormFile> files, Guid userId, Guid propertyId);
    }
}