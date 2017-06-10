namespace Landlords.Permissions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class MustOwnAccountAttribute : BasePermissionsAttribute
    {
        public MustOwnAccountAttribute() : base(typeof(MustOwnAccountChecker))
        {
        }

        private class MustOwnAccountChecker : Attribute, IAsyncResourceFilter
        {
            private readonly ILLDbContext _context;

            public MustOwnAccountChecker(ILLDbContext context)
            {
                _context = context;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                var userId = context.HttpContext.User.GetUserId();
                var portfolioId = context.HttpContext.User.GetPortfolioId();
                var accountId = TryParse(context.HttpContext.Request.Path.Value.Split('/').LastOrDefault())
                                ?? TryParse(context.HttpContext.Request.Query["entityId"]);

                if (!await userId.OwnsAccountAsync(portfolioId, accountId.GetValueOrDefault(), _context))
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