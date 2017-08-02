namespace Landlords.Controllers
{
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Permissions;
    using ViewModels;
    using System;
    using Database;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    public class ConversationController : Controller
    {
        private readonly IConversationDataProvider _conversationDataProvider;
        private readonly ILLDbContext _context;

        public ConversationController(IConversationDataProvider conversationDataProvider, ILLDbContext context)
        {
            _conversationDataProvider = conversationDataProvider;
            _context = context;
        }

        [HttpGet]
        [Permission(Permissions_CO.ViewId, Permissions_CO.ViewRouteId, Permissions_CO.ViewDescription)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _conversationDataProvider.GetConversationAsync(User.GetUserId()));
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Permission(Permissions_CO.SendId, Permissions_CO.SendRouteId, Permissions_CO.SendDescription)]
        public async Task<IActionResult> Post([FromBody] ConversationMessageViewModel value)
        {
            if (ModelState.IsValid)
            {
                if (value.ConversationId.IsDefault() || value.TenantId.IsDefault() || value.LandlordId.IsDefault())
                    return BadRequest("Unable to validate payload");

                if (!await value.TenantId.HasARelationshipAsync(value.LandlordId, _context))
                    return BadRequest("Unable to validate payload");

                if (!await _context.Conversations.AnyAsync(c => (c.LandlordId == value.TenantId && c.TenantId == value.LandlordId) || (c.LandlordId == value.LandlordId && c.TenantId == value.TenantId)))
                    return BadRequest("Unable to validate payload");
                
                return Ok(await _conversationDataProvider.SendMessageAsync(value));
            }

            return BadRequest(new { Errors = ModelState.ToErrorCollection() });
        }
    }
}