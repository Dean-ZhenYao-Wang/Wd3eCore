using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Email
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageEmailSettings = new Permission("ManageEmailSettings", "Manage Email Settings");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageEmailSettings
            }
            .AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageEmailSettings }
                },
            };
        }
    }
}
