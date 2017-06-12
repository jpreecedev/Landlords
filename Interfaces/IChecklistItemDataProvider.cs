namespace Landlords
{
    using System;
    using System.Threading.Tasks;

    public interface IChecklistItemDataProvider
    {
        Task ToggleChecklistItemCompletedAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, bool completed);
        Task MoveAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, string direction);
        Task ApplyTemplatePayloadAsync(Guid portfolioId, Guid checklistId, Guid checklistItemId, string payload);
    }
}