using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Settings
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageSettings = new Permission("ManageSettings", "Manage settings");

        // This permission is not exposed, it's just used for the APIs to generate/check custom ones
        public static readonly Permission ManageGroupSettings = new Permission("ManageResourceSettings", "Manage settings", new[] { ManageSettings });

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageSettings }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageSettings }
                }
            };
        }
    }
}
