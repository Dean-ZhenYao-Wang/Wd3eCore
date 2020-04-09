using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 当在一个类上使用时，只有在特定的特性被启用时，它才会包含服务。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RequireFeaturesAttribute : Attribute
    {
        public RequireFeaturesAttribute(string featureName)
        {
            RequiredFeatureNames = new string[] { featureName };
        }

        public RequireFeaturesAttribute(string featureName, params string[] otherFeatureNames)
        {
            var list = new List<string>(otherFeatureNames);
            list.Add(featureName);

            RequiredFeatureNames = list;
        }

        /// <summary>
        /// 所需特性的名称。
        /// </summary>
        public IList<string> RequiredFeatureNames { get; }

        public static IList<string> GetRequiredFeatureNamesForType(Type type)
        {
            var attribute = type.GetCustomAttributes<RequireFeaturesAttribute>(false).FirstOrDefault();

            return attribute?.RequiredFeatureNames ?? Array.Empty<string>();
        }
    }
}
