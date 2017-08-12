namespace Landlords.Interfaces
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Model.Database;
    using ViewModels;

    public interface IConversationDataProvider
    {
        Task<ConversationViewModel> NewConversationAsync(ApplicationUser applicationUser, ContactViewModel contact);
        Task<ConversationOverviewViewModel> GetConversationOverviewAsync(ClaimsPrincipal user, ApplicationUser applicationUser);
        Task<ConversationMessageViewModel> SendMessageAsync(Guid senderId, ConversationMessageViewModel message);
        Task SeenMessageAsync(Guid userId, Guid conversationId, Guid conversationMessageId);
    }
}