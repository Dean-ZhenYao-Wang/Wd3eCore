using Wd3eCore.Users.Workflows.Activities;

namespace Wd3eCore.Users.Workflows.ViewModels
{
    public class UserCreatedEventViewModel : UserEventViewModel<UserCreatedEvent>
    {
        public UserCreatedEventViewModel()
        {
        }

        public UserCreatedEventViewModel(UserCreatedEvent activity)
        {
            Activity = activity;
        }
    }
}
