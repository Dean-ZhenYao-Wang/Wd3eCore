using System.Threading.Tasks;
using Wd3eCore.Environment.Extensions;

namespace Wd3eCore.Admin
{
    public interface IAdminThemeService
    {
        Task<IExtensionInfo> GetAdminThemeAsync();
        Task SetAdminThemeAsync(string themeName);
        Task<string> GetAdminThemeNameAsync();
    }
}
