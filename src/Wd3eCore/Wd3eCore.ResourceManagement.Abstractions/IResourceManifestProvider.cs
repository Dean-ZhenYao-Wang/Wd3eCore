namespace Wd3eCore.ResourceManagement
{
    public interface IResourceManifestProvider
    {
        void BuildManifests(IResourceManifestBuilder builder);
    }
}
