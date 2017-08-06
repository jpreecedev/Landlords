﻿namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using ViewModels;
    using Database;
    using Notifications;
    using System;
    using System.Linq;

    [Route("api/[controller]")]
    public class ConversationController : Controller
    {
        private readonly IConversationDataProvider _conversationDataProvider;
        private readonly ILLDbContext _context;
        private readonly NotificationsMessageHandler _messageHandler;

        public ConversationController(IConversationDataProvider conversationDataProvider, ILLDbContext context, NotificationsMessageHandler messageHandler)
        {
            _conversationDataProvider = conversationDataProvider;
            _context = context;
            _messageHandler = messageHandler;
        }

        [HttpGet]
        [Permission(Permissions_CO.ViewId, Permissions_CO.ViewRouteId, Permissions_CO.ViewDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _conversationDataProvider.GetConversationOverviewAsync(User, User.GetApplicationUser(_context)));
        }

        [HttpPost("new"), ValidateAntiForgeryToken]
        [Permission(Permissions_CO.NewId, Permissions_CO.NewRouteId, Permissions_CO.NewDescription)]
        public async Task<IActionResult> New([FromBody] ContactViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (value.UserId.IsDefault())
                    return BadRequest("Unable to validate payload");

                return Ok(await _conversationDataProvider.NewConversationAsync(User.GetApplicationUser(_context), value));
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_CO.SendId, Permissions_CO.SendRouteId, Permissions_CO.SendDescription)]
        public async Task<IActionResult> Post([FromBody] ConversationMessageViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (value.ConversationId.IsDefault() || value.SenderId.IsDefault())
                    return BadRequest("Unable to validate payload");

                var sentMessage = await _conversationDataProvider.SendMessageAsync(value);
                await _messageHandler.SendChatMessageToRecipientAsync(Request.Query["access_token"].FirstOrDefault(), sentMessage);

                return Ok(sentMessage);
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}