using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wd3eCore.BackgroundTasks
{
    public interface IBackgroundTask
    {
        Task DoWorkAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken);
    }
}
