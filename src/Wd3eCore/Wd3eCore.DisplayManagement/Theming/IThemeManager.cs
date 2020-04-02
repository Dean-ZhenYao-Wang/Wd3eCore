using System.Threading.Tasks;
using Wd3eCore.Environment.Extensions;

namespace Wd3eCore.DisplayManagement.Theming
{
    public interface IThemeManager
    {
        Task<IExtensionInfo> GetThemeAsync();
    }
}
