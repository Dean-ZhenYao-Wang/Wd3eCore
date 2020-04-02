using System.Collections.Generic;

namespace Wd3eCore.ResourceManagement
{
    public interface IResourceManifestState
    {
        IEnumerable<ResourceManifest> ResourceManifests { get; set; }
    }

    public class ResourceManifestState : IResourceManifestState
    {
        public IEnumerable<ResourceManifest> ResourceManifests { get; set; }
    }
}
