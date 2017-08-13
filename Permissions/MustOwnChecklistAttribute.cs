namespace Landlords.Permissions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class MustOwnChecklistAttribute : BasePermissionsAttribute
    {
        public MustOwnChecklistAttribute() : base(typeof(MustOwnPropertyDetailsChecker))
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
                var portfolioId = context.HttpContext.User.GetPortfolioId();
                var checklistId = TryParse(context.HttpContext.Request.Query["checklistId"]);

                if (!await portfolioId.OwnsChecklistAsync(checklistId.GetValueOrDefault(), _context))
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