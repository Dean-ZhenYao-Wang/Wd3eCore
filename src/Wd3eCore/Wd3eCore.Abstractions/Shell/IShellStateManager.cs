using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.State;

namespace Wd3eCore.Environment.Shell
{
    public interface IShellStateManager
    {
        Task<ShellState> GetShellStateAsync();
        Task UpdateEnabledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value);
        Task UpdateInstalledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value);
    }
}
