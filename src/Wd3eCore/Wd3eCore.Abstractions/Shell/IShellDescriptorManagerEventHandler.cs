using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell
{
    /// <summary>
    /// 表示shell描述符的事件处理程序。
    /// </summary>
    public interface IShellDescriptorManagerEventHandler
    {
        /// <summary>
        /// 当shell描述符发生变化时触发。
        /// </summary>
        Task Changed(ShellDescriptor descriptor, string tenant);
    }
}
