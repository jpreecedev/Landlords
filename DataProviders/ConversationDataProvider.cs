namespace Landlords.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Database;
    using Landlords.Interfaces;
    using Landlords.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Model.Database;
    using Landlords.Core;
    using Model.Entities;

    public class ConversationDataProvider : BaseDataProvider, IConversationDataProvider
    {
        public ConversationDataProvider(IHostingEnvironment hostingEnvironment, LLDbContext context) : base(hostingEnvironment, context)
        {
        }

        public async Task<ConversationOverviewViewModel> GetConversationOverviewAsync(ClaimsPrincipal user, ApplicationUser applicationUser)
        {
            return new ConversationOverviewViewModel()
            {
                Contacts = await GetContactsForUserAsync(user, applicationUser),
                Conversations = await GetConversationsAsync(applicationUser)
            };
        }

        public async Task<ConversationViewModel> NewConversationAsync(ApplicationUser applicationUser, ContactViewModel contact)
        {
            //TODO: Check the instigator has the right to start a conversation with the receiver
            var entity = new Conversation
            {
                ReceiverId = contact.UserId,
                SenderId = applicationUser.Id,
                Created = DateTime.Now
            };

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();

            var sender = await Context.Users.SingleAsync(c => c.Id == entity.SenderId);
            var receiver = await Context.Users.SingleAsync(c => c.Id == entity.ReceiverId);

            return new ConversationViewModel
            {
                ConversationId = entity.Id,
                Messages = new List<ConversationMessageViewModel>(),
                SenderId = entity.SenderId,
                SenderFirstName = sender.FirstName,
                SenderLastName = sender.LastName,
                ReceiverId = entity.ReceiverId,
                ReceiverFirstName = receiver.FirstName,
                ReceiverLastName = receiver.LastName
            };
        }

        public async Task<ConversationMessageViewModel> SendMessageAsync(ConversationMessageViewModel message)
        {
            //TODO: Check the instigator has the right to post a message to this conversation
            var entity = new ConversationMessage
            {
                ConversationId = message.ConversationId,
                Created = DateTime.Now,
                Message = message.Message,
                SenderId = message.SenderId
            };

            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            
            return new ConversationMessageViewModel
            {
                Id = entity.Id,
                ConversationId = entity.ConversationId,
                Message = entity.Message,
                SenderId = entity.SenderId,
                ReceiverId = Context.Conversations.Single(c => c.Id == message.ConversationId).ReceiverId,
                Sent = entity.Created,
                Seen = entity.Seen
            };
        }

        private async Task<ICollection<ConversationViewModel>> GetConversationsAsync(ApplicationUser applicationUser)
        {
            var conversations = await (Context.Conversations.Include(x => x.Sender).Include(x => x.Receiver)
                .Where(c => c.Sender.Id == applicationUser.Id || c.Receiver.Id == applicationUser.Id)
                .Select(c => new ConversationViewModel
                {
                    ConversationId = c.Id,
                    SenderId = c.Sender.Id,
                    SenderFirstName = c.Sender.FirstName,
                    SenderLastName = c.Sender.LastName,
                    ReceiverId = c.Receiver.Id,
                    ReceiverFirstName = c.Receiver.FirstName,
                    ReceiverLastName = c.Receiver.LastName
                })).ToListAsync();

            conversations.ForEach(c =>
            {
                c.Messages = (Context.ConversationMessages.Where(m => m.ConversationId == c.ConversationId)
                    .Select(m => new ConversationMessageViewModel
                    {
                        Id = m.Id,
                        ConversationId = m.ConversationId,
                        Message = m.Message,
                        SenderId = m.SenderId,
                        Sent = m.Created,
                        Seen = m.Seen
                    })).ToList();
            });
            
            return conversations;
        }

        private async Task<ICollection<ContactViewModel>> GetContactsForUserAsync(ClaimsPrincipal user, ApplicationUser applicationUser)
        {
            if (user.IsTenant())
                return await GetContactsForTenantAsync(applicationUser);

            if (user.IsLandlord())
                return await GetContactsForLandlordAsync(applicationUser);

            if (user.IsAgencyUser())
                return await GetContactsForAgencyAsync(applicationUser);

            return new List<ContactViewModel>();
        }

        private async Task<ICollection<ContactViewModel>> GetContactsForTenantAsync(ApplicationUser applicationUser)
        {
            //untested
            var siteAdministrator = new ContactViewModel(await Context.Users.GetSiteAdministratorAsync());

            var portfolioManagers = await (from user in Context.Users.AsNoTracking()
                join aup in Context.ApplicationUserPortfolios on user.Id equals aup.UserId into aupJoin
                from applicationUserPortfolio in aupJoin
                join portfolio in Context.Portfolios on applicationUserPortfolio.PortfolioId equals portfolio.Id
                join propertyDetails in Context.PropertyDetails on portfolio.Id equals propertyDetails.PortfolioId
                join tenancy in Context.Tenancies on propertyDetails.Id equals tenancy.PropertyDetailsId into tenanciesJoin
                from t in tenanciesJoin
                join tenantTenancy in Context.TenantTenancies on t.Id equals tenantTenancy.TenancyId
                join tenant in Context.Tenants on tenantTenancy.TenantId equals tenant.Id
                where tenant.ApplicationUserId == applicationUser.Id
                select new ContactViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = user.Id
                }).ToListAsync();

            var agencyContacts = await (from user in Context.Users.AsNoTracking()
                join aup in Context.ApplicationUserPortfolios on user.Id equals aup.UserId into aupJoin
                from applicationUserPortfolio in aupJoin
                join appUser in Context.Users on applicationUserPortfolio.AgencyId equals appUser.AgencyId
                where portfolioManagers.Any(c => c.UserId == user.Id)
                select new ContactViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = user.Id
                }).ToListAsync();

            var result = new List<ContactViewModel>
            {
                siteAdministrator,
            };
            result.AddRange(portfolioManagers);
            result.AddRange(agencyContacts);
            return result;
        }

        private async Task<ICollection<ContactViewModel>> GetContactsForLandlordAsync(ApplicationUser applicationUser)
        {
            //untested

            var siteAdministrator = new ContactViewModel(await Context.Users.GetSiteAdministratorAsync());

            var tenants = await (from user in Context.Users.AsNoTracking()
                join tenant in Context.Tenants on user.Id equals tenant.ApplicationUserId
                join tenantTenancy in Context.TenantTenancies on tenant.Id equals tenantTenancy.TenantId
                join tenancy in Context.Tenancies on tenantTenancy.TenancyId equals tenancy.Id into tenanciesJoin
                from t in tenanciesJoin
                join propertyDetails in Context.PropertyDetails on t.PropertyDetailsId equals propertyDetails.Id
                join portfolio in Context.Portfolios on propertyDetails.PortfolioId equals portfolio.Id
                join aup in Context.ApplicationUserPortfolios on portfolio.Id equals aup.PortfolioId into aupJoin
                from applicationUserPortfolio in aupJoin
                join appUser in Context.Users on applicationUserPortfolio.UserId equals appUser.Id
                where appUser.Id == applicationUser.Id
                select new ContactViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = user.Id
                }
            ).ToListAsync();

            var agencyContacts = await (from user in Context.Users.AsNoTracking()
                join aup in Context.ApplicationUserPortfolios on user.Id equals aup.UserId into aupJoin
                from applicationUserPortfolio in aupJoin
                join appUser in Context.Users on applicationUserPortfolio.AgencyId equals appUser.AgencyId
                where user.Id == applicationUser.Id
                select new ContactViewModel
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    UserId = appUser.Id
                }).ToListAsync();

            var result = new List<ContactViewModel>
            {
                siteAdministrator
            };
            result.AddRange(agencyContacts);
            result.AddRange(tenants);
            return result;
        }

        private async Task<ICollection<ContactViewModel>> GetContactsForAgencyAsync(ApplicationUser applicationUser)
        {
            //untested

            var siteAdministrator = new ContactViewModel(await Context.Users.GetSiteAdministratorAsync());

            var landlords = await (from user in Context.Users.AsNoTracking()
                join aup in Context.ApplicationUserPortfolios on user.Id equals aup.UserId into aupJoin
                from applicationUserPortfolio in aupJoin
                join appUser in Context.Users on applicationUserPortfolio.AgencyId equals appUser.AgencyId
                where user.AgencyId == applicationUser.AgencyId
                select new ContactViewModel
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    UserId = appUser.Id
                }).ToListAsync();

            var tenants = await (from user in Context.Users.AsNoTracking()
                join tenant in Context.Tenants on user.Id equals tenant.ApplicationUserId
                join tenantTenancy in Context.TenantTenancies on tenant.Id equals tenantTenancy.TenantId
                join tenancy in Context.Tenancies on tenantTenancy.TenancyId equals tenancy.Id into tenanciesJoin
                from t in tenanciesJoin
                join propertyDetails in Context.PropertyDetails on t.PropertyDetailsId equals propertyDetails.Id
                join portfolio in Context.Portfolios on propertyDetails.PortfolioId equals portfolio.Id
                join aup in Context.ApplicationUserPortfolios on portfolio.Id equals aup.PortfolioId into aupJoin
                from applicationUserPortfolio in aupJoin
                join appUser in Context.Users on applicationUserPortfolio.UserId equals appUser.Id
                where appUser.AgencyId == applicationUser.AgencyId
                select new ContactViewModel
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    UserId = appUser.Id
                }
            ).ToListAsync();

            var result = new List<ContactViewModel>
            {
                siteAdministrator
            };
            result.AddRange(landlords);
            result.AddRange(tenants);
            return result;
        }
    }
}