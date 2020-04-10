using Microsoft.AspNetCore.Http;
using Wd3eCore.Environment.Shell.Builders;

namespace Wd3eCore.Environment.Shell
{
    /// <summary>
    /// 用于捕捉shell上下文和原始路径信息。
    /// </summary>
    public class ShellContextFeature
    {
        /// <summary>
        /// 当前shell的上下文。
        /// </summary>
        public ShellContext ShellContext { get; set; }

        /// <summary>
        /// 原有的路径基础。
        /// </summary>
        public PathString OriginalPathBase { get; set; }

        /// <summary>
        /// 原有的路径。
        /// </summary>
        public PathString OriginalPath { get; set; }
    }
}
