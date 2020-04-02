using System;
using System.Linq;
using Wd3eCore.DisplayManagement.Extensions;
using Wd3eCore.DisplayManagement.Manifest;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Modules.Manifest;

namespace Wd3eCore.DisplayManagement.Events
{
    public class ThemeFeatureBuilderEvents : FeatureBuilderEvents
    {
        public override void Building(FeatureBuildingContext context)
        {
            var moduleInfo = context.ExtensionInfo.Manifest.ModuleInfo;

            if (moduleInfo is ThemeAttribute || (moduleInfo is ModuleMarkerAttribute &&
                moduleInfo.Type.Equals("Theme", StringComparison.OrdinalIgnoreCase)))
            {
                var extensionInfo = new ThemeExtensionInfo(context.ExtensionInfo);

                if (extensionInfo.HasBaseTheme())
                {
                    context.FeatureDependencyIds = context
                        .FeatureDependencyIds
                        .Concat(new[] { extensionInfo.BaseTheme })
                        .ToArray();
                }

                context.ExtensionInfo = extensionInfo;
            }
        }
    }
}
