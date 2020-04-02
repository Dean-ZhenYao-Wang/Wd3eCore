using Wd3eCore.Environment.Extensions;

namespace Wd3eCore.DisplayManagement.Extensions
{
    public static class ExtensionInfoExtensions
    {
        public static bool IsTheme(this IExtensionInfo extensionInfo)
        {
            return extensionInfo is IThemeExtensionInfo;
        }
    }
}
