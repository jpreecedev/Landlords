namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using Landlords.Interfaces;
    using Landlords.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Model.Entities;

    public class ConversationDataProvider : BaseDataProvider, IConversationDataProvider
    {
        public ConversationDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ICollection<ConversationViewModel>> GetConversationAsync(Guid userId)
        {
            //untested

            return await (Context.Conversations.AsNoTracking()
                    .Include(x => x.Landlord).Include(x => x.Tenant)
                    .Where(c => c.LandlordId == userId || c.TenantId == userId))
                .Select(c => new ConversationViewModel
                {
                    TenantId = c.TenantId,
                    LandlordId = c.LandlordId,
                    ConversationId = c.Id,
                    LandlordFirstName = c.Landlord.FirstName,
                    LandlordLastName = c.Landlord.LastName,
                    TenantFirstName = c.Tenant.FirstName,
                    TenantLastName = c.Tenant.LastName,
                    Messages = Context.ConversationMessages.Where(message => message.ConversationId == c.Id)
                        .OrderBy(d => d.Created)
                        .Select(d => new ConversationMessageViewModel
                        {
                            Sent = d.Created,
                            Id = d.Id,
                            TenantId = c.TenantId,
                            LandlordId = c.LandlordId,
                            Message = d.Message,
                            ConversationId = c.Id,
                            SenderId = d.FromId,
                            Seen = d.Seen
                        }).ToList()
                })
                .ToListAsync();
        }

        public async Task<ConversationMessageViewModel> SendMessageAsync(ConversationMessageViewModel message)
        {
            //untested
            var entity = new ConversationMessage
            {
                ConversationId = message.ConversationId,
                Created = DateTime.Now,
                FromId = message.SenderId,
                Message = message.Message,
            };

            await Context.ConversationMessages.AddAsync(entity);
            await Context.SaveChangesAsync();

            return new ConversationMessageViewModel
            {
                ConversationId = entity.ConversationId,
                Id = entity.Id,
                TenantId = entity.Conversation.TenantId,
                LandlordId = entity.Conversation.LandlordId,
                Message = entity.Message,
                Sent = entity.Created,
                SenderId = entity.FromId,
                Seen = entity.Seen
            };
        }
    }
}