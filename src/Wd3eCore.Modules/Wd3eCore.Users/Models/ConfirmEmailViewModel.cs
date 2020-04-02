using Wd3eCore.DisplayManagement.Views;

namespace Wd3eCore.Users.ViewModels
{
    public class ConfirmEmailViewModel : ShapeViewModel
    {
        public ConfirmEmailViewModel()
        {
            Metadata.Type = "TemplateUserConfirmEmail";
        }

        public IUser User { get; set; }
        public string ConfirmEmailUrl { get; set; }
    }
}
