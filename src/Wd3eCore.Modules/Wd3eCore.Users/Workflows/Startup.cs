using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Modules;
using Wd3eCore.Users.Handlers;
using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Users.Workflows.Drivers;
using Wd3eCore.Users.Workflows.Handlers;
using Wd3eCore.Workflows.Helpers;

namespace Wd3eCore.Users.Workflows
{
    [RequireFeatures("Wd3eCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<RegisterUserTask, RegisterUserTaskDisplay>();
            services.AddActivity<UserCreatedEvent, UserCreatedEventDisplay>();
            services.AddActivity<UserEnabledEvent, UserEnabledEventDisplay>();
            services.AddActivity<UserDisabledEvent, UserDisabledEventDisplay>();
            services.AddActivity<UserLoggedInEvent, UserLoggedInEventDisplay>();
            services.AddScoped<IUserEventHandler, UserEventHandler>();
            services.AddActivity<AssignUserRoleTask, AssignUserRoleTaskDisplay>();
        }
    }
}
