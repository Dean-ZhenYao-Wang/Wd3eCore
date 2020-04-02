using Microsoft.AspNetCore.Http;
using Wd3eCore.Environment.Shell.Builders;

namespace Wd3eCore.Environment.Shell
{
    /// <summary>
    /// Used to capture the shell context and original path infos.
    /// </summary>
    public class ShellContextFeature
    {
        /// <summary>
        /// The current shell context.
        /// </summary>
        public ShellContext ShellContext { get; set; }

        /// <summary>
        /// The original path base.
        /// </summary>
        public PathString OriginalPathBase { get; set; }

        /// <summary>
        /// The original path.
        /// </summary>
        public PathString OriginalPath { get; set; }
    }
}
