using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Security
{
    public class StandardPermissions
    {
        public static readonly Permission SiteOwner = new Permission("SiteOwner", "Site Owners Permission", isSecurityCritical: true);
    }
}
