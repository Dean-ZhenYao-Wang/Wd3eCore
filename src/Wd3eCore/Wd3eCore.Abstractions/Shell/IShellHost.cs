using System.Collections.Generic;
using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.Builders;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Environment.Shell
{
    public interface IShellHost
    {
        /// <summary>
        /// 确保所有的<see cref="ShellContext"/>都是预先创建的，并且可用来处理请求。
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// 返回一个现有的<see cref="ShellContext"/>，或者在必要时创建一个新的。
        /// </summary>
        /// <param name="settings"><see cref="ShellSettings"/>对象代表要获取的shell。</param>
        /// <returns></returns>
        Task<ShellContext> GetOrCreateShellContextAsync(ShellSettings settings);

        /// <summary>
        /// 创建一个独立的服务作用域，可用于解析本地服务。
        /// </summary>
        /// <param name="settings"><see cref="ShellSettings"/>对象代表要获取的shell。</param>
        Task<ShellScope> GetScopeAsync(ShellSettings settings);

        /// <summary>
        /// 更新现有的shell配置。
        /// </summary>
        /// <param name="settings"></param>
        Task UpdateShellSettingsAsync(ShellSettings settings);

        /// <summary>
        /// 重载shell.
        /// </summary>
        /// <param name="settings"></param>
        Task ReloadShellContextAsync(ShellSettings settings);

        /// <summary>
        /// 创建一个新的 <see cref="ShellContext"/>。
        /// </summary>
        /// <param name="settings"><see cref="ShellSettings"/>对象代表要创建的shell。</param>
        /// <returns></returns>
        Task<ShellContext> CreateShellContextAsync(ShellSettings settings);

        /// <summary>
        /// 列出所有可用的 <see cref="ShellContext"/>实例。
        /// 一个shell可能已经被发布或尚未构建，
        /// 如果是这样，'shell.Released'为true，'shell.CreateScope()'返回null，
        /// 但仍然可以使用'GetScopeAsync(shell.Settings)'。
        /// </summary>
        /// <remarks>如果一个shell还没有被创建，比如说它已经被删除了，还没有被重新创建，可能就不会被列出。</remarks>
        IEnumerable<ShellContext> ListShellContexts();

        /// <summary>
        /// 尝试检索与指定租户相关的shell设置。
        /// </summary>
        /// <returns><c>true</c> 如果可以找到设置,则为true <c>false</c> 否则为false.</returns>
        bool TryGetSettings(string name, out ShellSettings settings);

        /// <summary>
        /// 检索所有的shell设置。
        /// </summary>
        /// <returns>所有的shell设置。</returns>
        IEnumerable<ShellSettings> GetAllSettings();
    }
}
