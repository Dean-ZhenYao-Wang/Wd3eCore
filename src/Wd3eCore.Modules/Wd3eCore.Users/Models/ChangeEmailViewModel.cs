using System.ComponentModel.DataAnnotations;

namespace Wd3eCore.Users.ViewModels
{
    public class ChangeEmailViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
