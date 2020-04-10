using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wd3eCore.Environment.Shell
{
    public interface IShellSettingsManager
    {
        /// <summary>
        /// 根据配置创建一个默认的shell设置。
        /// </summary>
        ShellSettings CreateDefaultSettings();

        /// <summary>
        /// 检索存储的所有shell设置。
        /// </summary>
        /// <returns>所有shell设置</returns>
        Task<IEnumerable<ShellSettings>> LoadSettingsAsync();

        /// <summary>
        /// 检索给定租户的设置。
        /// </summary>
        /// <returns>shell设置</returns>
        Task<ShellSettings> LoadSettingsAsync(string tenant);

        /// <summary>
        /// 将shell设置保存到存储中。
        /// </summary>
        /// <param name="settings">要存储的shell设置。</param>
        Task SaveSettingsAsync(ShellSettings settings);
    }
}
