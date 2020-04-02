namespace Wd3eCore.ResourceManagement
{
    public interface IResourceManifestBuilder
    {
        ResourceManifest Add();

        ResourceManifest Add(ResourceManifest manifest);
    }
}
