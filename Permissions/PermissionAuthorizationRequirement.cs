namespace Landlords.Permissions
{
    using System;
    using Microsoft.AspNetCore.Authorization;

    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(Guid requiredPermissions)
        {
            RequiredPermission = requiredPermissions;
        }

        public Guid RequiredPermission { get; }
    }
}