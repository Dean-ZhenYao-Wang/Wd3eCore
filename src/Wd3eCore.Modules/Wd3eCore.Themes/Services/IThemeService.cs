using System.Threading.Tasks;

namespace Wd3eCore.Themes.Services
{
    public interface IThemeService
    {
        Task DisableThemeFeaturesAsync(string themeName);
        Task EnableThemeFeaturesAsync(string themeName);
    }
}
