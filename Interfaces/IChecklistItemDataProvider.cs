namespace Landlords
{
    using System;
    using System.Threading.Tasks;

    public interface IChecklistItemDataProvider
    {
        Task ToggleChecklistItemCompletedAsync(Guid userId, Guid checklistId, Guid checklistItemId, bool completed);
        Task ToggleChecklistItemExpandedAsync(Guid userId, Guid checklistId, Guid checklistItemId, bool expanded);
    }
}