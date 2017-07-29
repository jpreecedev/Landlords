namespace Landlords.ViewModels
{
    using Model.Entities;
    using System;

    public class NotificationViewModel
    {
        public NotificationViewModel()
        {
            
        }

        public NotificationViewModel(Notification notification)
        {
            if (notification == null)
            {
                return;
            }

            PropertyDetailsId = notification.PropertyDetailsId;
            PortfolioId = notification.PortfolioId;
            TenancyId = notification.TenancyId;
            TenantId = notification.TenantId;
            ShortlistedPropertyId = notification.ShortlistedPropertyId;
            Type = notification.Type;
            Message = notification.Message;
        }

        public Guid? PropertyDetailsId { get; set; }

        public PropertyDetails PropertyDetails { get; set; }
        
        public Portfolio Portfolio { get; set; }

        public Guid? PortfolioId { get; set; }
        
        public Tenancy Tenancy { get; set; }

        public Guid? TenancyId { get; set; }

        public Guid? TenantId { get; set; }

        public Tenant Tenant { get; set; }

        public Guid? ShortlistedPropertyId { get; set; }

        public ShortlistedProperty ShortlistedProperty { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }

        public string SecondaryMessage { get; set; }
    }
}