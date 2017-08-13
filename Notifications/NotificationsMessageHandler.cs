namespace Landlords.Notifications
{
    using System.Threading.Tasks;
    using Interfaces;
    using System;
    using Core;
    using Microsoft.AspNetCore.Authorization;
    using ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    public class NotificationsMessageHandler : WebSocketHandler
    {
        private readonly INotificationsDataProvider _notificationsDataProvider;
        private readonly IAuthorizationService _authService;

        public NotificationsMessageHandler(INotificationsDataProvider notificationsDataProvider, IAuthorizationService authService, WebSocketConnectionManager webSocketConnectionManager)
            : base(webSocketConnectionManager)
        {
            _notificationsDataProvider = notificationsDataProvider;
            _authService = authService;
        }

        public async Task GetAllNotificationsAsync(ClaimsPrincipal user)
        {
            var nameClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(nameClaim.Value);
            var connections = WebSocketConnectionManager.GetConnectionIdsByUserId(userId);

            if (!connections.Any())
                return;

            var notifications = await GetNotificationsAsync(user, userId);
            foreach (var userConnection in connections)
            {
                await InvokeClientMethodAsync(userConnection, "GetAllNotifications", new[] { notifications });
            }
        }

        public async Task GetAllNotifications(string accessToken, string connectionId)
        {
            var tokenValidation = accessToken.ToJwtValidationResult();
            if (!tokenValidation.IsValid)
            {
                throw new UnauthorizedAccessException();
            }

            var nameClaim = tokenValidation.User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(nameClaim.Value);

            var notifications = await GetNotificationsAsync(tokenValidation.User, userId);
            await InvokeClientMethodAsync(Guid.Parse(connectionId), "GetAllNotifications", new[] {notifications});
        }
        
        public async Task StartNewConversationAsync(string accessToken, Guid receiverId, ConversationViewModel conversation)
        {
            var cloned = conversation.ShallowClone();
            cloned.IsToReceiver = true;

            foreach (var userClient in WebSocketConnectionManager.GetClientByUserId(receiverId))
            {
                await InvokeClientMethodAsync(userClient.Key, "StartNewConversation", new[] { cloned });
            }
        }

        public async Task SendChatMessageToRecipientAsync(string accessToken, Guid senderId, ConversationMessageViewModel conversationMessage)
        {
            foreach (var userClient in WebSocketConnectionManager.GetClientByUserId(conversationMessage.ReceiverId))
            {
                await InvokeClientMethodAsync(userClient.Key, "ChatMessageReceived", new[] { conversationMessage });
            }

            foreach (var userClient in WebSocketConnectionManager.GetClientByUserId(senderId))
            {
                await InvokeClientMethodAsync(userClient.Key, "ChatMessageReceived", new[] { conversationMessage });
            }
        }

        private async Task<ICollection<NotificationViewModel>> GetNotificationsAsync(ClaimsPrincipal user, Guid userId)
        {
            var result = new List<NotificationViewModel>();

            if (user.HasPropertyPortfolio())
            {
                var portfolioId = user.GetPortfolioId();
                result = await _notificationsDataProvider.GetNotificationsForPortfolioAsync(portfolioId);
                result.AddRange(await _notificationsDataProvider.GetNotificationsForUserAsync(userId));
            }
            else
            {
                result = await _notificationsDataProvider.GetNotificationsForUserAsync(userId);
            }

            return result;
        }
    }
}