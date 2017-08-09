namespace Landlords.Notifications
{
    using System.Threading.Tasks;
    using Interfaces;
    using System;
    using Core;
    using Microsoft.AspNetCore.Authorization;
    using ViewModels;
    using System.Collections.Generic;
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

        public async Task GetAllNotifications(string accessToken, string connectionId)
        {
            var tokenValidation = accessToken.ToJwtValidationResult();
            if (!tokenValidation.IsValid)
            {
                throw new UnauthorizedAccessException();
            }

            List<NotificationViewModel> notifications;
            if (tokenValidation.User.HasPropertyPortfolio())
            {
                var portfolioId = tokenValidation.User.GetPortfolioId();
                notifications = await _notificationsDataProvider.GetNotificationsForPortfolioAsync(portfolioId);
            }
            else
            {
                var nameClaim = tokenValidation.User.FindFirst(ClaimTypes.NameIdentifier);
                var userId = Guid.Parse(nameClaim.Value);
                notifications = _notificationsDataProvider.GetNotificationsForUserAsync(userId);
            }
            
            await InvokeClientMethodAsync(connectionId, "GetAllNotifications", new[] {notifications});
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
    }
}