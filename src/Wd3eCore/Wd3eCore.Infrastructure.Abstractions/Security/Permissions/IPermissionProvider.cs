using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wd3eCore.Security.Permissions
{
    /// <summary>
    /// 由模块实现，用于枚举可能被授予的权限类型。
    /// </summary>
    public interface IPermissionProvider
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync();
        IEnumerable<PermissionStereotype> GetDefaultStereotypes();
    }

    public class PermissionStereotype
    {
        public string Name { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}
