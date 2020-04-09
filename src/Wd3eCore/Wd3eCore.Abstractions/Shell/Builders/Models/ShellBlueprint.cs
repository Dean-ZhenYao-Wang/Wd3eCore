using System;
using System.Collections.Generic;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell.Builders.Models
{
    /// <summary>
    /// 包含为特定租户初始化IoC容器所需的信息。
    /// 这个模型由ICompositionStrategy创建，并传递到IShellContainerFactory。
    /// </summary>
    public class ShellBlueprint
    {
        public ShellSettings Settings { get; set; }
        public ShellDescriptor Descriptor { get; set; }

        public IDictionary<Type, FeatureEntry> Dependencies { get; set; }
    }
}
