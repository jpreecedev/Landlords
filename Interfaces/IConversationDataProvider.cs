namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface IConversationDataProvider
    {
        Task<ICollection<ConversationViewModel>> GetConversationAsync(Guid userId);
        Task SendMessageAsync(ConversationMessageViewModel message);
    }
}