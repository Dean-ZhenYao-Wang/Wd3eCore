using System.ComponentModel.DataAnnotations;

namespace Wd3eCore.Users.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string PasswordConfirmation { get; set; }

        public string ResetToken { get; set; }
    }
}
