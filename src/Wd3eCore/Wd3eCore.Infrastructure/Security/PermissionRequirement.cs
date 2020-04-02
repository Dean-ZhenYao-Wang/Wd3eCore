using System;
using Microsoft.AspNetCore.Authorization;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Security
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(Permission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            Permission = permission;
        }

        public Permission Permission { get; set; }
    }
}
