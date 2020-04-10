using System.Collections.Generic;
using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell.Descriptor
{
    /// <summary>
    /// 服务从shell容器中解析出来。主要由主机使用。
    /// </summary>
    public interface IShellDescriptorManager
    {
        /// <summary>
        /// 使用shell特定的数据库或其他资源来返回当前的 "正确 "配置。
        /// 主机将使用这些信息来重新初始化shell。
        /// </summary>
        Task<ShellDescriptor> GetShellDescriptorAsync();

        /// <summary>
        /// 修改数据库信息以匹配作为参数传递的信息。
        /// 以前的SerialNumber用于乐观并发，如果存储中的数字与提供的数字不匹配，应该抛出异常。
        /// </summary>
        Task UpdateShellDescriptorAsync(
            int priorSerialNumber,
            IEnumerable<ShellFeature> enabledFeatures,
            IEnumerable<ShellParameter> parameters);
    }
}
