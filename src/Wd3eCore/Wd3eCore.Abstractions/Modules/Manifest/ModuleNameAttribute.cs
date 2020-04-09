using System;

namespace Wd3eCore.Modules.Manifest
{
    /// <summary>
    /// 列出引用模块的包或项目名，在构建时自动生成。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class ModuleNameAttribute : Attribute
    {
        public ModuleNameAttribute(string name)
        {
            Name = name ?? String.Empty;
        }

        /// <Summary>
        /// 引用模块的包或项目名。
        /// </Summary>
        public string Name { get; }
    }
}
