using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Users.Services;
using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Users.Workflows.ViewModels;
using Wd3eCore.Workflows.Display;

namespace Wd3eCore.Users.Workflows.Drivers
{
    public class UserDisabledEventDisplay : ActivityDisplayDriver<UserDisabledEvent, UserDisabledEventViewModel>
    {
        public UserDisabledEventDisplay(IUserService userService)
        {
            UserService = userService;
        }

        protected IUserService UserService { get; }

        protected override void EditActivity(UserDisabledEvent source, UserDisabledEventViewModel target)
        {
        }

        public override IDisplayResult Display(UserDisabledEvent activity)
        {
            return Combine(
                Shape("UserDisabledEvent_Fields_Thumbnail", new UserDisabledEventViewModel(activity)).Location("Thumbnail", "Content"),
                Factory("UserDisabledEvent_Fields_Design", ctx =>
                {
                    var shape = new UserDisabledEventViewModel();
                    shape.Activity = activity;

                    return shape;
                }).Location("Design", "Content")
            );
        }
    }
}
