using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Users.Services;
using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Users.Workflows.ViewModels;
using Wd3eCore.Workflows.Display;

namespace Wd3eCore.Users.Workflows.Drivers
{
    public class UserEnabledEventDisplay : ActivityDisplayDriver<UserEnabledEvent, UserEnabledEventViewModel>
    {
        public UserEnabledEventDisplay(IUserService userService)
        {
            UserService = userService;
        }

        protected IUserService UserService { get; }

        protected override void EditActivity(UserEnabledEvent source, UserEnabledEventViewModel target)
        {
        }

        public override IDisplayResult Display(UserEnabledEvent activity)
        {
            return Combine(
                Shape("UserEnabledEvent_Fields_Thumbnail", new UserEnabledEventViewModel(activity)).Location("Thumbnail", "Content"),
                Factory("UserEnabledEvent_Fields_Design", ctx =>
                {
                    var shape = new UserEnabledEventViewModel();
                    shape.Activity = activity;

                    return shape;
                }).Location("Design", "Content")
            );
        }
    }
}
