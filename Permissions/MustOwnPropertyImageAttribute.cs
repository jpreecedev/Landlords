﻿namespace Landlords.Permissions
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Database;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class MustOwnPropertyImageAttribute : TypeFilterAttribute
    {
        public MustOwnPropertyImageAttribute() : base(typeof(MustOwnPropertyImageChecker))
        {
        }

        private class MustOwnPropertyImageChecker : Attribute, IAsyncResourceFilter
        {
            private readonly ILLDbContext _context;

            public MustOwnPropertyImageChecker(ILLDbContext context)
            {
                _context = context;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                var userId = context.HttpContext.User.GetUserId();
                var propertyImageId = Guid.Parse(context.HttpContext.Request.Query["entityId"]);

                if (!await userId.OwnsPropertyImageAsync(propertyImageId, _context))
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