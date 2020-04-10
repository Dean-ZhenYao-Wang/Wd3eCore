using System.Collections.Generic;

namespace Wd3eCore.Scripting
{
    /// <summary>
    /// <see cref="IGlobalMethodProvider"/>的一个实现为<see cref="IScriptingManager"/>实例提供了自定义方法。
    /// </summary>
    public interface IGlobalMethodProvider
    {
        /// <summary>
        /// 获取要提供给<see cref="IScriptingManager"/>的方法列表。
        /// </summary>
        /// <returns><see cref="GlobalMethod"/>实例的列表。</returns>
        IEnumerable<GlobalMethod> GetMethods();
    }
}
