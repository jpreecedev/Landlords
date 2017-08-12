namespace Landlords.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels;

    public interface INotificationsDataProvider
    {
        Task<NotificationViewModel> CreateNotification(Guid portfolioId, NotificationViewModel notification);
        Task Delete(Guid portfolioId, Guid notificationId);
        Task<List<NotificationViewModel>> GetNotificationsForPortfolioAsync(Guid portfolioId);
        Task<List<NotificationViewModel>> GetNotificationsForPropertyAsync(Guid portfolioId, Guid propertyId);
        Task<List<NotificationViewModel>> GetNotificationsForUserAsync(Guid userId);
    }
}