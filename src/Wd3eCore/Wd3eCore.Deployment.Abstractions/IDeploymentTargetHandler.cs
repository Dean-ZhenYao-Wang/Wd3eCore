using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace Wd3eCore.Deployment
{
    public interface IDeploymentTargetHandler
    {
        Task ImportFromFileAsync(IFileProvider fileProvider);
    }
}
