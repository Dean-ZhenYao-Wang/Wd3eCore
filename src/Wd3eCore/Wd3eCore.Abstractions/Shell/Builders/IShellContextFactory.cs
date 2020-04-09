using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell.Builders
{
    /// <summary>
    /// 高级协调器，它使用其他组件功能，在给定租户设置的情况下为正在运行的shell构建所有构件。
    /// </summary>
    public interface IShellContextFactory
    {
        /// <summary>
        /// 根据特定的租户设置结构构建shell上下文
        /// </summary>
        Task<ShellContext> CreateShellContextAsync(ShellSettings settings);

        /// <summary>
        /// 为未初始化的Wd3e实例构建shell上下文。需要显示设置用户界面。
        /// </summary>
        Task<ShellContext> CreateSetupContextAsync(ShellSettings settings);

        /// <summary>
        /// 给定特性和参数的特定描述，构建shell上下文。
        /// Shell当前的实际描述符没有作用。不使用或更新描述符缓存。
        /// </summary>
        Task<ShellContext> CreateDescribedContextAsync(ShellSettings settings, ShellDescriptor shellDescriptor);
    }
}
