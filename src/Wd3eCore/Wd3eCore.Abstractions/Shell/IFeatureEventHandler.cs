using System.Threading.Tasks;
using Wd3eCore.Environment.Extensions.Features;

namespace Wd3eCore.Environment.Shell
{
    public interface IFeatureEventHandler
    {
        Task InstallingAsync(IFeatureInfo feature);
        Task InstalledAsync(IFeatureInfo feature);
        Task EnablingAsync(IFeatureInfo feature);
        Task EnabledAsync(IFeatureInfo feature);
        Task DisablingAsync(IFeatureInfo feature);
        Task DisabledAsync(IFeatureInfo feature);
        Task UninstallingAsync(IFeatureInfo feature);
        Task UninstalledAsync(IFeatureInfo feature);
    }
}
