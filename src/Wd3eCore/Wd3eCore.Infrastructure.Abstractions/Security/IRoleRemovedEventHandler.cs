using System.Threading.Tasks;

namespace Wd3eCore.Security
{
    public interface IRoleRemovedEventHandler
    {
        Task RoleRemovedAsync(string roleName);
    }
}
