using Wd3eCore.Users.Workflows.Activities;

namespace Wd3eCore.Users.Workflows.ViewModels
{
    public class UserDisabledEventViewModel : UserEventViewModel<UserDisabledEvent>
    {
        public UserDisabledEventViewModel()
        {
        }

        public UserDisabledEventViewModel(UserDisabledEvent activity)
        {
            Activity = activity;
        }
    }
}
