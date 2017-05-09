namespace Landlords.Permissions
{
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Database;

    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly ApplicationUserManager _userManager;

        public PermissionAuthorizationHandler(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            var currentUserPermissions = await _userManager.GetUserPermissionsAsync(context.User.GetUserId());
            var authorized = currentUserPermissions.Any(c => c.PermissionId == requirement.RequiredPermission);
            
            if (authorized)
            {
                context.Succeed(requirement);
            }
        }
    }
}