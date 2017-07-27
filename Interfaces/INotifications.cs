namespace Landlords.Interfaces
{
    using System.Collections.Generic;
    using ViewModels;

    public interface INotifications
    {
        ICollection<NotificationViewModel> Notifications { get; set; }
    }
}