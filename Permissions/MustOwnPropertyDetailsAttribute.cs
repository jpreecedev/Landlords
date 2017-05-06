namespace Landlords.Permissions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class MustOwnPropertyDetailsAttribute : TypeFilterAttribute
    {
        public MustOwnPropertyDetailsAttribute() : base(typeof(MustOwnPropertyDetailsChecker))
        {
        }

        private class MustOwnPropertyDetailsChecker : Attribute, IAsyncResourceFilter
        {
            private readonly ILLDbContext _context;

            public MustOwnPropertyDetailsChecker(ILLDbContext context)
            {
                _context = context;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                var userId = context.HttpContext.User.GetUserId();
                var propertyId = Guid.Parse(context.HttpContext.Request.Path.Value.Split('/').Last());

                if (!await userId.OwnsPropertyDetailsAsync(propertyId, _context))
                {
                    context.Result = new ChallengeResult();
                }
                else
                {
                    await next();
                }
            }
        }
    }
}