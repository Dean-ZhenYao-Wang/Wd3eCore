using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Users.Models;

namespace Wd3eCore.Users.ViewModels
{
    public class LostPasswordViewModel : ShapeViewModel
    {
        public LostPasswordViewModel()
        {
            Metadata.Type = "TemplateUserLostPassword";
        }

        public User User { get; set; }
        public string LostPasswordUrl { get; set; }
    }
}
