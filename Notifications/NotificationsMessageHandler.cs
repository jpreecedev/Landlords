namespace Landlords.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces;
    using ViewModels;
    using System;
    using Core;
    using Microsoft.AspNetCore.Authorization;

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
            var tokenValidation = IsTokenValid(accessToken);
            if (!tokenValidation.IsValid)
            {
                throw new UnauthorizedAccessException();
            }

            var portfolioId = tokenValidation.User.GetPortfolioId();
            var notifications = await _notificationsDataProvider.GetNotificationsForPortfolioAsync(portfolioId);

            await InvokeClientMethodAsync(connectionId, "GetAllNotifications", new []{ notifications });
        }
    }
}