namespace Landlords.Permissions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class RequiresPermissionAttribute : TypeFilterAttribute
    {
        public RequiresPermissionAttribute(params string[] permissions) : base(typeof(RequiresPermissionAttributeChecker))
        {
            Arguments = new[] {new PermissionAuthorizationRequirement(permissions)};
        }

        private class RequiresPermissionAttributeChecker : Attribute, IAsyncResourceFilter
        {
            private readonly IAuthorizationService _authService;
            private readonly PermissionAuthorizationRequirement _permissionRequirement;

            public RequiresPermissionAttributeChecker(IAuthorizationService authService, PermissionAuthorizationRequirement permissionRequirement)
            {
                _authService = authService;
                _permissionRequirement = permissionRequirement;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                if (!await _authService.AuthorizeAsync(context.HttpContext.User, context.ActionDescriptor.ToString(), _permissionRequirement))
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