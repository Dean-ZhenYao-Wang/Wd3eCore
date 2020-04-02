using Microsoft.Extensions.Localization;
using Wd3eCore.Users.Services;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Users.Workflows.Activities
{
    public class UserDisabledEvent : UserEvent
    {
        public UserDisabledEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<UserDisabledEvent> localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(UserDisabledEvent);

        public override LocalizedString DisplayText => S["User Disabled Event"];
    }
}
