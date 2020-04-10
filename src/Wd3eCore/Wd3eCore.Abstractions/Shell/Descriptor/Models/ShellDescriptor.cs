using System.Collections.Generic;

namespace Wd3eCore.Environment.Shell.Descriptor.Models
{
    /// <summary>
    /// 包含租户启用的特性的快照。
    /// 信息通过IShellDescriptorManager从shell中提取出来，并传递给ICompositionStrategy来构建shell蓝图
    /// </summary>
    public class ShellDescriptor
    {
        /// <summary>
        /// 获取或设置shell描述符的版本号。
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary>
        /// 获取或设置shell中的特性列表。
        /// </summary>
        public IList<ShellFeature> Features { get; set; } = new List<ShellFeature>();

        /// <summary>
        /// 获取或设置特定于此shell的参数列表。
        /// </summary>
        public IList<ShellParameter> Parameters { get; set; } = new List<ShellParameter>();
    }
}
