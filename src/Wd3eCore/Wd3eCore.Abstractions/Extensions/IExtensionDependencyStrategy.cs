using Wd3eCore.Environment.Extensions.Features;

namespace Wd3eCore.Environment.Extensions
{
    public interface IExtensionDependencyStrategy
    {
        bool HasDependency(IFeatureInfo observer, IFeatureInfo subject);
    }
}
