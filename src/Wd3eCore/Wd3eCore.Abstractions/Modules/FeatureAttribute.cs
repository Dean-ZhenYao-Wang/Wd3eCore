using System;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 可以通过名称将服务或组件的属性与特定特性关联。
    /// 该组件只有在该特性被启用时才会被使用。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FeatureAttribute : Attribute
    {
        public FeatureAttribute(string featureName)
        {
            FeatureName = featureName;
        }

        /// <summary>
        /// 要分配组件的属性的名称。
        /// </summary>
        public string FeatureName { get; set; }
    }
}
