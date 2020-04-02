using Wd3eCore.Users.Workflows.Activities;

namespace Wd3eCore.Users.Workflows.ViewModels
{
    public class UserLoggedInEventViewModel : UserEventViewModel<UserLoggedInEvent>
    {
        public UserLoggedInEventViewModel()
        {
        }

        public UserLoggedInEventViewModel(UserLoggedInEvent activity)
        {
            Activity = activity;
        }
    }
}
