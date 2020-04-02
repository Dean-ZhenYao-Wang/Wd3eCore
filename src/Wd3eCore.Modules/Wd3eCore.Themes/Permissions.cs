using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Themes
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ApplyTheme = new Permission("ApplyTheme") { Description = "Apply a Theme" };

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ApplyTheme
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
                    Permissions = new[] { ApplyTheme }
                },
            };
        }
    }
}
