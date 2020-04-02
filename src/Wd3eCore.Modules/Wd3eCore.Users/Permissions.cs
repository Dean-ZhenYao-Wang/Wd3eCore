using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Users
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageUsers = new Permission("ManageUsers", "Managing Users");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageUsers
            }
            .AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {ManageUsers}
                },
            };
        }
    }
}
