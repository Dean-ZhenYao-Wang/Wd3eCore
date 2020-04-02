using System.Collections.Generic;
using System.Threading.Tasks;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Admin
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission AccessAdminPanel = new Permission("AccessAdminPanel", "Access admin panel");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(GetPermissions());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = GetPermissions()
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = GetPermissions()
                },
                new PermissionStereotype {
                    Name = "Moderator",
                    Permissions = GetPermissions()
                },
                new PermissionStereotype {
                    Name = "Author",
                    Permissions = GetPermissions()
                },
                new PermissionStereotype {
                    Name = "Contributor",
                    Permissions = GetPermissions()
                }
            };
        }

        private IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                AccessAdminPanel
            };
        }
    }
}
