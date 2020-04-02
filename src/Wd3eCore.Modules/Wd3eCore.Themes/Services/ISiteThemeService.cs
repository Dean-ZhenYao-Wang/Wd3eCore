using System.Threading.Tasks;
using Wd3eCore.Environment.Extensions;

namespace Wd3eCore.Themes.Services
{
    public interface ISiteThemeService
    {
        Task<IExtensionInfo> GetSiteThemeAsync();
        Task SetSiteThemeAsync(string themeName);
        Task<string> GetCurrentThemeNameAsync();
    }
}
