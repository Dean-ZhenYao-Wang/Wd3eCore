using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Wd3eCore.Environment.Extensions.Features
{
    public class FeaturesProvider : IFeaturesProvider
    {
        public const string FeatureProviderCacheKey = "FeatureProvider:Features";

        private readonly IEnumerable<IFeatureBuilderEvents> _featureBuilderEvents;

        private readonly ILogger L;

        public FeaturesProvider(
            IEnumerable<IFeatureBuilderEvents> featureBuilderEvents,
            ILogger<FeaturesProvider> logger)
        {
            _featureBuilderEvents = featureBuilderEvents;
            L = logger;
        }

        public IEnumerable<IFeatureInfo> GetFeatures(
            IExtensionInfo extensionInfo,
            IManifestInfo manifestInfo)
        {
            var featuresInfos = new List<IFeatureInfo>();

            // 功能和依赖项包含在本节中
            var features = manifestInfo.ModuleInfo.Features.ToList();
            if (features.Count > 0)
            {
                foreach (var feature in features)
                {
                    if (String.IsNullOrWhiteSpace(feature.Id))
                    {
                        throw new ArgumentException(
                            $"A feature is missing a mandatory 'Id' property in the Module '{extensionInfo.Id}'/在模块“{extensionInfo.Id}”中缺少一个强制的“Id”属性");
                    }

                    var featureId = feature.Id;
                    var featureName = feature.Name ?? feature.Id;

                    var featureDependencyIds = feature.Dependencies
                        .Select(e => e.Trim()).ToArray();

                    if (!int.TryParse(feature.Priority ?? manifestInfo.ModuleInfo.Priority, out int featurePriority))
                    {
                        featurePriority = 0;
                    }

                    var featureCategory = feature.Category ?? manifestInfo.ModuleInfo.Category;
                    var featureDescription = feature.Description ?? manifestInfo.ModuleInfo.Description;
                    var featureDefaultTenantOnly = feature.DefaultTenantOnly;
                    var featureIsAlwaysEnabled = feature.IsAlwaysEnabled;

                    var context = new FeatureBuildingContext
                    {
                        FeatureId = featureId,
                        FeatureName = featureName,
                        Category = featureCategory,
                        Description = featureDescription,
                        ExtensionInfo = extensionInfo,
                        ManifestInfo = manifestInfo,
                        Priority = featurePriority,
                        FeatureDependencyIds = featureDependencyIds,
                        DefaultTenantOnly = featureDefaultTenantOnly,
                        IsAlwaysEnabled = featureIsAlwaysEnabled
                    };

                    foreach (var builder in _featureBuilderEvents)
                    {
                        builder.Building(context);
                    }

                    var featureInfo = new FeatureInfo(
                        featureId,
                        featureName,
                        featurePriority,
                        featureCategory,
                        featureDescription,
                        extensionInfo,
                        featureDependencyIds,
                        featureDefaultTenantOnly,
                        featureIsAlwaysEnabled);

                    foreach (var builder in _featureBuilderEvents)
                    {
                        builder.Built(featureInfo);
                    }

                    featuresInfos.Add(featureInfo);
                }
            }
            else
            {
                // The Extension has only one feature, itself, and that can have dependencies
                var featureId = extensionInfo.Id;
                var featureName = manifestInfo.Name;

                var featureDependencyIds = manifestInfo.ModuleInfo.Dependencies
                    .Select(e => e.Trim()).ToArray();

                if (!int.TryParse(manifestInfo.ModuleInfo.Priority, out int featurePriority))
                {
                    featurePriority = 0;
                }

                var featureCategory = manifestInfo.ModuleInfo.Category;
                var featureDescription = manifestInfo.ModuleInfo.Description;
                var featureDefaultTenantOnly = manifestInfo.ModuleInfo.DefaultTenantOnly;
                var featureIsAlwaysEnabled = manifestInfo.ModuleInfo.IsAlwaysEnabled;

                var context = new FeatureBuildingContext
                {
                    FeatureId = featureId,
                    FeatureName = featureName,
                    Category = featureCategory,
                    Description = featureDescription,
                    ExtensionInfo = extensionInfo,
                    ManifestInfo = manifestInfo,
                    Priority = featurePriority,
                    FeatureDependencyIds = featureDependencyIds,
                    DefaultTenantOnly = featureDefaultTenantOnly,
                    IsAlwaysEnabled = featureIsAlwaysEnabled
                };

                foreach (var builder in _featureBuilderEvents)
                {
                    builder.Building(context);
                }

                var featureInfo = new FeatureInfo(
                    context.FeatureId,
                    context.FeatureName,
                    context.Priority,
                    context.Category,
                    context.Description,
                    context.ExtensionInfo,
                    context.FeatureDependencyIds,
                    context.DefaultTenantOnly,
                    context.IsAlwaysEnabled);

                foreach (var builder in _featureBuilderEvents)
                {
                    builder.Built(featureInfo);
                }

                featuresInfos.Add(featureInfo);
            }

            return featuresInfos;
        }
    }
}
