using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Tenants
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageTenants = new Permission("ManageTenants", "Manage tenants");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageTenants }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageTenants }
                }
            };
        }
    }
}
