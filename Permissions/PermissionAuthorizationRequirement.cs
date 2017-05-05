namespace Landlords.Permissions
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Model;

    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(IEnumerable<string> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }

        public IEnumerable<string> RequiredPermissions { get; }
    }
}