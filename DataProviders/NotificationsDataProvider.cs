namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Model.DataTypes;
    using Model.Entities;
    using ViewModels;

    public class NotificationsDataProvider : BaseDataProvider, INotificationsDataProvider
    {
        public NotificationsDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<NotificationViewModel> CreateNotification(Guid portfolioId, NotificationViewModel notification)
        {
            var entity = new Notification
            {
                Created = DateTime.Now,
                PortfolioId = portfolioId,
                Type = notification.Type,
                Message = notification.Message,
                PropertyDetailsId = notification.PropertyDetailsId,
                ShortlistedPropertyId = notification.ShortlistedPropertyId,
                TenancyId = notification.TenancyId,
                TenantId = notification.TenantId
            };

            await Context.Notifications.AddAsync(entity);
            await Context.SaveChangesAsync();

            return new NotificationViewModel(entity);
        }

        public async Task<List<NotificationViewModel>> GetNotificationsForPropertyAsync(Guid portfolioId, Guid propertyDetailsId)
        {
            return await (from notification in Context.Notifications.AsNoTracking()
                          join propertyDetails in Context.PropertyDetails on notification.PropertyDetailsId equals propertyDetails.Id
                          where notification.PortfolioId == portfolioId && notification.PropertyDetailsId == propertyDetailsId && !notification.IsDeleted
                          select new NotificationViewModel
                          {
                              Message = notification.Message,
                              SecondaryMessage = propertyDetails.Reference,
                              Type = notification.Type,
                              PortfolioId = notification.PortfolioId,
                              PropertyDetailsId = notification.PropertyDetailsId,
                              PropertyDetails = propertyDetails,
                              ShortlistedPropertyId = notification.ShortlistedPropertyId,
                              TenancyId = notification.TenancyId,
                              TenantId = notification.TenantId
                          })
                .ToListAsync();
        }

        public async Task<List<NotificationViewModel>> GetNotificationsForPortfolioAsync(Guid portfolioId)
        {
            return await (from notification in Context.Notifications.AsNoTracking()
                          join propertyDetails in Context.PropertyDetails on notification.PropertyDetailsId.GetValueOrDefault() equals propertyDetails.Id into propertyDetailsJoin
                          where notification.PortfolioId == portfolioId && !notification.IsDeleted
                          select new NotificationViewModel
                          {
                              Message = notification.Message,
                              SecondaryMessage = propertyDetailsJoin.Any() ? propertyDetailsJoin.First().Reference : null,
                              Type = notification.Type,
                              PortfolioId = notification.PortfolioId,
                              PropertyDetailsId = notification.PropertyDetailsId,
                              ShortlistedPropertyId = notification.ShortlistedPropertyId,
                              TenancyId = notification.TenancyId,
                              TenantId = notification.TenantId
                          })
                .ToListAsync();
        }

        public async Task Delete(Guid portfolioId, Guid notificationId)
        {
            var notification = await Context.Notifications.SingleOrDefaultAsync(c => c.PortfolioId == portfolioId && c.Id == notificationId);
            if (notification != null)
            {
                notification.Deleted = DateTime.Now;
            }
        }

        public async Task<List<NotificationViewModel>> GetNotificationsForUserAsync(Guid userId)
        {
            //TODO
            var result = new List<NotificationViewModel>();

            var lastMessageSeen = await (from conversationMessage in Context.ConversationMessages.AsNoTracking()
                    join conversation in Context.Conversations on conversationMessage.ConversationId equals conversation.Id
                    orderby conversationMessage.Created descending 
                    where conversation.ReceiverId == userId || conversationMessage.ReceiverId == userId
                    select new
                    {
                        ConversationMessage = conversationMessage,
                        Conversation = conversation
                    })
                .FirstOrDefaultAsync();

            if (lastMessageSeen != null && lastMessageSeen.ConversationMessage.Seen == null)
            {
                result.Add(new NotificationViewModel
                {
                    Message = "You have unread messages",
                    Type = NotificationType.Message
                });
            }

            return result;
        }
    }
}