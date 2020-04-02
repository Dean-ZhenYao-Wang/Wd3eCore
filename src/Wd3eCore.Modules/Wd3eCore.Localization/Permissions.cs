using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// Represents the localization module permissions.
    /// </summary>
    public class Permissions : IPermissionProvider
    {
        /// <summary>
        /// Gets a permession for managing the cultures.
        /// </summary>
        public static readonly Permission ManageCultures = new Permission("ManageCultures", "Manage supported culture");

        /// <inheritdocs />
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageCultures }.AsEnumerable());
        }

        /// <inheritdocs />
        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageCultures }
                },
                new PermissionStereotype
                {
                    Name = "Editor",
                    Permissions = new[] { ManageCultures }
                }
            };
        }
    }
}
