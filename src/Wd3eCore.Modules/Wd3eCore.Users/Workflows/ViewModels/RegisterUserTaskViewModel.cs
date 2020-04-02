using System.ComponentModel.DataAnnotations;

namespace Wd3eCore.Users.Workflows.ViewModels
{
    public class RegisterUserTaskViewModel
    {
        public bool SendConfirmationEmail { get; set; }

        [Required]
        public string ConfirmationEmailTemplate { get; set; }
    }
}
