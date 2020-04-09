using System;
using System.Linq;

namespace Wd3eCore.Modules.Manifest
{
    /// <summary>
    /// 在模块中定义一个特性，可以多次使用。
    /// 如果至少定义了一个特性，则忽略模块默认特性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class FeatureAttribute : Attribute
    {
        public FeatureAttribute()
        {
        }

        public bool Exists => Id != null;

        /// <Summary>特性的标识符</Summary>
        public string Id { get; set; }

        /// <Summary>
        /// 可读的特性名称。如果没有提供，将使用标识符。
        /// </Summary>
        public string Name { get; set; }

        /// <Summary>简要总结一下这个特性。</Summary>
        public string Description { get; set; } = String.Empty;

        /// <Summary>
        /// 特性所依赖的特性列表
        /// 它的驱动程序/处理程序会在依赖关系的驱动/处理程序之后调用。
        /// </Summary>
        public string[] Dependencies { get; set; } = Enumerable.Empty<string>().ToArray();

        /// <Summary>
        /// 不破坏依赖顺序的特性的优先级。
        /// 高者为先 后面的驱动/处理程序被调用。
        /// </Summary>
        public string Priority { get; set; } = "0";

        /// <Summary>
        /// 特性所属的组(按类别)。
        /// 如果没有提供，默认为“Uncategorized”。
        /// </Summary>
        public string Category { get; set; }

        /// <summary>
        ///设置为<c>true</c>，只允许默认租户启用/禁用该特性。
        /// </summary>
        public bool DefaultTenantOnly { get; set; }

        /// <summary>
        /// 启用后，检查是否不能禁用该特性。默认值为false。
        /// </summary>
        public bool IsAlwaysEnabled { get; set; } = false;
    }
}
