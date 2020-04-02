using Microsoft.Extensions.Localization;
using Wd3eCore.Users.Services;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Users.Workflows.Activities
{
    public class UserEnabledEvent : UserEvent
    {
        public UserEnabledEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<UserEnabledEvent> localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(UserEnabledEvent);

        public override LocalizedString DisplayText => S["User Enabled Event"];
    }
}
