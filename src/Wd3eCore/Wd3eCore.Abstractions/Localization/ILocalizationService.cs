using System.Threading.Tasks;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// 表示本地化服务的契约。
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// 返回站点的默认文化。
        /// </summary>
        Task<string> GetDefaultCultureAsync();

        /// <summary>
        /// 返回站点支持的所有文化。它还包含默认的区域性。
        /// </summary>
        Task<string[]> GetSupportedCulturesAsync();
    }
}
