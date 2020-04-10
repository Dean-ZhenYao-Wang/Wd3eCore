using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Wd3eCore.Scripting
{
    /// <summary>
    /// <see cref="IScriptingManager"/>的一个实现提供了评估自定义脚本的服务。
    /// </summary>
    public interface IScriptingManager
    {
        /// <summary>
        /// 获取指定前缀的脚本引擎。
        /// </summary>
        /// <param name="prefix">代表要返回的引擎的字符串。</param>
        /// <returns>脚本引擎或<code>null</code>(如果找不到的话)。</returns>
        IScriptingEngine GetScriptingEngine(string prefix);

        /// <summary>
        /// 通过寻找匹配的脚本引擎，执行一些前缀脚本。
        /// </summary>
        /// <param name="directive">要执行的指令。</param>
        /// <param name="fileProvider">一个可选的<see cref="IFileProvider"/>实例。</param>
        /// <param name="basePath"></param>
        /// <param name="scopedMethodProviders">作用域为脚本评估的方法提供程序的列表。</param>
        /// <returns>The result of the script if any.</returns>
        object Evaluate(string directive, IFileProvider fileProvider, string basePath, IEnumerable<IGlobalMethodProvider> scopedMethodProviders);

        /// <summary>
        /// 这个<see cref="IScriptingManager"/>实例的可用方法提供程序列表。
        /// </summary>
        IList<IGlobalMethodProvider> GlobalMethodProviders { get; }
    }
}
