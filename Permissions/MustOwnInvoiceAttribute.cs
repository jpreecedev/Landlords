namespace Landlords.Permissions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class MustOwnInvoiceAttribute : BasePermissionsAttribute
    {
        public MustOwnInvoiceAttribute() : base(typeof(MustOwnInvoiceChecker))
        {
        }

        private class MustOwnInvoiceChecker : Attribute, IAsyncResourceFilter
        {
            private readonly ILLDbContext _context;

            public MustOwnInvoiceChecker(ILLDbContext context)
            {
                _context = context;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                var userId = context.HttpContext.User.GetUserId();
                var portfolioId = context.HttpContext.User.GetPortfolioId();
                var invoiceId = TryParse(context.HttpContext.Request.Path.Value.Split('/').LastOrDefault())
                                ?? TryParse(context.HttpContext.Request.Query["entityId"]);

                if (!await userId.OwnsInvoiceAsync(portfolioId, invoiceId.GetValueOrDefault(), _context))
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