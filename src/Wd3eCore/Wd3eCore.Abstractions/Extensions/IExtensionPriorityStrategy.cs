using Wd3eCore.Environment.Extensions.Features;

namespace Wd3eCore.Environment.Extensions
{
    public interface IExtensionPriorityStrategy
    {
        int GetPriority(IFeatureInfo feature);
    }
}
