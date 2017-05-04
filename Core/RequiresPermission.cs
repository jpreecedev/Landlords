namespace Landlords.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Model;

    public class RequiresPermissionAttribute : TypeFilterAttribute
    {
        public RequiresPermissionAttribute(params Permissions[] permissions) : base(typeof(RequiresPermissionAttributeImpl))
        {
            Arguments = new[] {new PermissionAuthorizationRequirement(permissions)};
        }
    }

    public class RequiresPermissionAttributeImpl : Attribute, IAsyncResourceFilter
    {
        private readonly IAuthorizationService _authService;
        private readonly ApplicationUserManager _userManager;
        private readonly PermissionAuthorizationRequirement _requiredPermissions;

        public RequiresPermissionAttributeImpl(IAuthorizationService authService, PermissionAuthorizationRequirement requiredPermissions)
        {
            _authService = authService;
            _requiredPermissions = requiredPermissions;

        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (!await _authService.AuthorizeAsync(context.HttpContext.User, context.ActionDescriptor.ToString(), _requiredPermissions))
            {
                context.Result = new ChallengeResult();
            }

            await next();
        }
    }

    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<Permissions> RequiredPermissions { get; }

        public PermissionAuthorizationRequirement(IEnumerable<Permissions> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }
    }
}