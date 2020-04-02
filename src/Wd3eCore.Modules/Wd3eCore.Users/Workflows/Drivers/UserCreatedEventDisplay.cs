using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Users.Services;
using Wd3eCore.Users.Workflows.Activities;
using Wd3eCore.Users.Workflows.ViewModels;
using Wd3eCore.Workflows.Display;

namespace Wd3eCore.Users.Workflows.Drivers
{
    public class UserCreatedEventDisplay : ActivityDisplayDriver<UserCreatedEvent, UserCreatedEventViewModel>
    {
        public UserCreatedEventDisplay(IUserService userService)
        {
            UserService = userService;
        }

        protected IUserService UserService { get; }

        protected override void EditActivity(UserCreatedEvent source, UserCreatedEventViewModel target)
        {
        }

        public override IDisplayResult Display(UserCreatedEvent activity)
        {
            return Combine(
                Shape("UserCreatedEvent_Fields_Thumbnail", new UserCreatedEventViewModel(activity)).Location("Thumbnail", "Content"),
                Factory("UserCreatedEvent_Fields_Design", ctx =>
                {
                    var shape = new UserCreatedEventViewModel();
                    shape.Activity = activity;

                    return shape;
                }).Location("Design", "Content")
            );
        }
    }
}
