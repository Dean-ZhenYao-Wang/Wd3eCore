using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Wd3eCore.Settings
{
    /// <summary>
    /// 提供管理站点设置的服务。
    /// </summary>
    public interface ISiteService
    {
        /// <summary>
        /// 返回udpate的站点设置。
        /// </summary>
        Task<ISite> LoadSiteSettingsAsync();

        /// <summary>
        /// 以只读形式返回当前租户的站点设置。
        /// </summary>
        Task<ISite> GetSiteSettingsAsync();

        /// <summary>
        /// 保存对站点设置的更改。
        /// </summary>
        Task UpdateSiteSettingsAsync(ISite site);

        /// <summary>
        /// 获取在站点设置更改时设置的更改令牌。
        /// </summary>
        IChangeToken ChangeToken { get; }
    }
}
