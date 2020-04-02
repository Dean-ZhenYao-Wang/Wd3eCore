using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Workflows.ViewModels;

namespace Wd3eCore.Users.Workflows.ViewModels
{
    public class UserEventViewModel<T> : ActivityViewModel<T> where T : UserEvent
    {
        public UserEventViewModel()
        {
        }

        public UserEventViewModel(T activity)
        {
            Activity = activity;
        }
    }
}
