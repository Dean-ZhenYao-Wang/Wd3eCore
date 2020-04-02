using System.Threading.Tasks;

namespace Wd3eCore.Environment.Shell.Builders
{
    public interface IShellPipeline
    {
        /// <summary>
        /// Executes this shell pipeline.
        /// </summary>
        Task Invoke(object context);
    }
}
