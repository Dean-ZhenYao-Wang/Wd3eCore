using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.Workflows.Activities;
using Wd3eCore.Workflows.Options;

namespace Wd3eCore.Workflows.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddActivity<TActivity, TDriver>(this IServiceCollection services) where TActivity : class, IActivity where TDriver : class, IDisplayDriver<IActivity>
        {
            services.Configure<WorkflowOptions>(options => options.RegisterActivity<TActivity, TDriver>());
        }
    }
}
