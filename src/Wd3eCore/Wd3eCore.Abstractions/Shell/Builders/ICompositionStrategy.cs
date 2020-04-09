using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.Builders.Models;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell.Builders
{
    /// <summary>
    /// 服务在主机级将可缓存的描述符转化为可加载的蓝图。
    /// </summary>
    public interface ICompositionStrategy
    {
        /// <summary>
        /// 使用来自 IExtensionManager 的信息，转换并填充 shell 构建者,正确初始化租户 IoC 容器所需的所有蓝图模型。
        /// </summary>
        Task<ShellBlueprint> ComposeAsync(ShellSettings settings, ShellDescriptor descriptor);
    }
}
