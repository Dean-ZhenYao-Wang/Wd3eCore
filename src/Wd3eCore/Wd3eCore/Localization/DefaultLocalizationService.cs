using System.Globalization;
using System.Threading.Tasks;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// 表示<see cref="ILocalizationService"/>的默认实现
    /// </summary>
    public class DefaultLocalizationService : ILocalizationService
    {
        private static readonly Task<string> DefaultCulture = Task.FromResult(CultureInfo.InstalledUICulture.Name);
        private static readonly Task<string[]> SupportedCultures = Task.FromResult(new[] { CultureInfo.InstalledUICulture.Name });

        /// <inheritdocs />
        public Task<string> GetDefaultCultureAsync() => DefaultCulture;

        /// <inheritdocs />
        public Task<string[]> GetSupportedCulturesAsync() => SupportedCultures;
    }
}
