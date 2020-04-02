using System.ComponentModel.DataAnnotations;

namespace Wd3eCore.Roles.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }
    }
}
