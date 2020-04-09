using System;
using System.Collections.Generic;
using System.Linq;

namespace Wd3eCore.Modules.Manifest
{
    /// <summary>
    /// 定义一个由特性组成的模块。
    /// 如果模块只有一个默认功能，可以在这里定义。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class ModuleAttribute : FeatureAttribute
    {
        public ModuleAttribute()
        {
        }

        public virtual string Type => "Module";
        public new bool Exists => Id != null;

        /// <Summary>
        /// 允许模块项目更改其“AssemblyName”而无需更新代码的逻辑id。如果没有提供，则使用程序集名称。
        /// </Summary>
        public new string Id { get; set; }

        /// <Summary>开发者的名字。</Summary>
        public string Author { get; set; } = String.Empty;

        /// <Summary>开发人员网站的URL。</Summary>
        public string Website { get; set; } = String.Empty;

        /// <Summary>SemVer格式的版本号。</Summary>
        public string Version { get; set; } = "0.0";

        /// <Summary>标签列表。</Summary>
        public string[] Tags { get; set; } = Enumerable.Empty<string>().ToArray();

        public List<FeatureAttribute> Features { get; } = new List<FeatureAttribute>();
    }
}
