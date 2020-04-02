using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Admin
{
    public class PermissionsAdminSettings : IPermissionProvider
    {
        public static readonly Permission ManageAdminSettings = new Permission("ManageAdminSettings", "Manage Admin Settings");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageAdminSettings }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageAdminSettings }
                }
            };
        }
    }
}
