using System.Threading.Tasks;
using Wd3eCore.Users.Handlers;
using Wd3eCore.Users.Models;
using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Users.Workflows.Handlers
{
    public class UserEventHandler : IUserEventHandler
    {
        private readonly IWorkflowManager _workflowManager;

        public UserEventHandler(IWorkflowManager workflowManager)
        {
            _workflowManager = workflowManager;
        }

        public Task CreatedAsync(UserContext context)
        {
            return TriggerWorkflowEventAsync(nameof(UserCreatedEvent), (User)context.User);
        }

        public Task DisabledAsync(UserContext context)
        {
            return TriggerWorkflowEventAsync(nameof(UserDisabledEvent), (User)context.User);
        }

        public Task EnabledAsync(UserContext context)
        {
            return TriggerWorkflowEventAsync(nameof(UserEnabledEvent), (User)context.User);
        }

        private Task TriggerWorkflowEventAsync(string name, User user)
        {
            return _workflowManager.TriggerEventAsync(name,
                input: new { User = user },
                correlationId: user.Id.ToString()
            );
        }
    }
}
