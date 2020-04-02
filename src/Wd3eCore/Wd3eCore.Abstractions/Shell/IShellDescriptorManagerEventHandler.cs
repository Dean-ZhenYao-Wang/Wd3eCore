using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell
{
    /// <summary>
    /// Represent an event handler for shell descriptor.
    /// </summary>
    public interface IShellDescriptorManagerEventHandler
    {
        /// <summary>
        /// Triggered whenever a shell descriptor has changed.
        /// </summary>
        Task Changed(ShellDescriptor descriptor, string tenant);
    }
}
