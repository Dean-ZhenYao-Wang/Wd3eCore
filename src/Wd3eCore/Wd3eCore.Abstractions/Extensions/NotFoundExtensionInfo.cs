using System.Collections.Generic;
using System.Linq;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Environment.Extensions.Manifests;

namespace Wd3eCore.Environment.Extensions
{
    public class NotFoundExtensionInfo : IExtensionInfo
    {
        private readonly IEnumerable<IFeatureInfo> _featureInfos;
        private readonly string _extensionId;
        private readonly IManifestInfo _manifestInfo;

        public NotFoundExtensionInfo(string extensionId)
        {
            _featureInfos = Enumerable.Empty<IFeatureInfo>();
            _extensionId = extensionId;
            _manifestInfo = new NotFoundManifestInfo(extensionId);
        }

        public bool Exists => false;

        public IEnumerable<IFeatureInfo> Features => _featureInfos;

        public string Id => _extensionId;

        public IManifestInfo Manifest => _manifestInfo;

        public string SubPath => _extensionId;
    }
}
