using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Security;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Roles
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageRoles = new Permission("ManageRoles", "Managing Roles", isSecurityCritical: true);
        public static readonly Permission AssignRoles = new Permission("AssignRoles", "Assign Roles", new[] { ManageRoles }, isSecurityCritical: true);

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageRoles, AssignRoles, StandardPermissions.SiteOwner
            }
            .AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] { ManageRoles, AssignRoles, StandardPermissions.SiteOwner }
                },
            };
        }
    }
}
