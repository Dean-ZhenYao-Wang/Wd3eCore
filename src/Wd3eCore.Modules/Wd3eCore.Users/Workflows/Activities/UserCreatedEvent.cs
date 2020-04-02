using Microsoft.Extensions.Localization;
using Wd3eCore.Users.Services;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Users.Workflows.Activities
{
    public class UserCreatedEvent : UserEvent
    {
        public UserCreatedEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<UserCreatedEvent> localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(UserCreatedEvent);

        public override LocalizedString DisplayText => S["User Created Event"];
    }
}
