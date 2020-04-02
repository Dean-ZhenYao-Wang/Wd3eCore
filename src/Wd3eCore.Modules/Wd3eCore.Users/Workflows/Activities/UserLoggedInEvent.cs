using Microsoft.Extensions.Localization;
using Wd3eCore.Users.Services;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Users.Workflows.Activities
{
    public class UserLoggedInEvent : UserEvent
    {
        public UserLoggedInEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<UserLoggedInEvent> localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(UserLoggedInEvent);

        public override LocalizedString DisplayText => S["User Loggedin Event"];
    }
}
