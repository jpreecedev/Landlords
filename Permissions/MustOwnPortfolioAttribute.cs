namespace Landlords.Permissions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class MustOwnPortfolioAttribute : TypeFilterAttribute
    {
        public MustOwnPortfolioAttribute() : base(typeof(MustOwnPortfolioChecker))
        {
        }

        private class MustOwnPortfolioChecker : Attribute, IAsyncResourceFilter
        {
            private readonly ILLDbContext _context;

            public MustOwnPortfolioChecker(ILLDbContext context)
            {
                _context = context;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                var userId = context.HttpContext.User.GetUserId();
                var portfolioId = context.HttpContext.User.GetPortfolioId();

                if (!await userId.OwnsPortfolioAsync(portfolioId, _context))
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