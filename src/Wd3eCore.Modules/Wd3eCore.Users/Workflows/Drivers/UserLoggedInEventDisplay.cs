using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Users.Services;
using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Users.Workflows.ViewModels;
using Wd3eCore.Workflows.Display;

namespace Wd3eCore.Users.Workflows.Drivers
{
    public class UserLoggedInEventDisplay : ActivityDisplayDriver<UserLoggedInEvent, UserLoggedInEventViewModel>
    {
        public UserLoggedInEventDisplay(IUserService userService)
        {
            UserService = userService;
        }

        protected IUserService UserService { get; }

        protected override void EditActivity(UserLoggedInEvent source, UserLoggedInEventViewModel target)
        {
        }

        public override IDisplayResult Display(UserLoggedInEvent activity)
        {
            return Combine(
                Shape("UserLoggedInEvent_Fields_Thumbnail", new UserLoggedInEventViewModel(activity)).Location("Thumbnail", "Content"),
                Factory("UserLoggedInEvent_Fields_Design", ctx =>
                {
                    var shape = new UserLoggedInEventViewModel();
                    shape.Activity = activity;

                    return shape;
                }).Location("Design", "Content")
            );
        }
    }
}
