using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Environment.Extensions.Manifests;

namespace Wd3eCore.Environment.Extensions
{
    public class InternalExtensionInfo : IExtensionInfo
    {
        private readonly string _id;
        private readonly string _subPath;
        private readonly IManifestInfo _manifestInfo;
        private readonly IEnumerable<IFeatureInfo> _features;

        public InternalExtensionInfo(string subPath)
        {
            _id = Path.GetFileName(subPath);
            _subPath = subPath;

            _manifestInfo = new NotFoundManifestInfo(subPath);
            _features = Enumerable.Empty<IFeatureInfo>();
        }

        public string Id => _id;
        public string SubPath => _subPath;
        public IManifestInfo Manifest => _manifestInfo;
        public IEnumerable<IFeatureInfo> Features => _features;
        public bool Exists => _manifestInfo.Exists;
    }
}
