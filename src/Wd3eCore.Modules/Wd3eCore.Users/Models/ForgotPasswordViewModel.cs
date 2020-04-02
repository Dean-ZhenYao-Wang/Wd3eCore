using System.ComponentModel.DataAnnotations;

namespace Wd3eCore.Users.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string UserIdentifier { get; set; }
    }
}
