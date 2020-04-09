using System.Threading.Tasks;

namespace Wd3eCore.Environment.Shell.Builders
{
    public interface IShellPipeline
    {
        /// <summary>
        /// 执行这个shell管道。
        /// </summary>
        Task Invoke(object context);
    }
}
