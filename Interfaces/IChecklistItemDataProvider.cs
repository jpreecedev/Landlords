namespace Landlords
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using ViewModels;

    public interface IChecklistItemDataProvider
    {
        Task ToggleChecklistItemCompletedAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, bool completed);
        Task MoveAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, string direction);
        Task ApplyTemplatePayloadAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, string payload);
        Task<ICollection<ResourceViewModel>> UploadAsync(ICollection<IFormFile> files, Guid checklistId, Guid checklistItemId);
    }
}