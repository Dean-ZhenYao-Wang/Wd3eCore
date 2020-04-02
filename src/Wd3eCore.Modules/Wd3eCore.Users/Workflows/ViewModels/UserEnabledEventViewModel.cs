using Wd3eCore.Users.Workflows.Activities;

namespace Wd3eCore.Users.Workflows.ViewModels
{
    public class UserEnabledEventViewModel : UserEventViewModel<UserEnabledEvent>
    {
        public UserEnabledEventViewModel()
        {
        }

        public UserEnabledEventViewModel(UserEnabledEvent activity)
        {
            Activity = activity;
        }
    }
}
