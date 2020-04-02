using System.Collections.Generic;

namespace Wd3eCore.Environment.Extensions.Features
{
    public interface IFeaturesProvider
    {
        IEnumerable<IFeatureInfo> GetFeatures(
            IExtensionInfo extensionInfo,
            IManifestInfo manifestInfo);
    }
}
