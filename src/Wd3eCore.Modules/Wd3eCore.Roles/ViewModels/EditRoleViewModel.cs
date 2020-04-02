using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wd3eCore.Security;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Roles.ViewModels
{
    public class EditRoleViewModel
    {
        public string Name { get; set; }
        public string RoleDescription { get; set; }
        public IDictionary<string, IEnumerable<Permission>> RoleCategoryPermissions { get; set; }
        public IEnumerable<string> EffectivePermissions { get; set; }

        [BindNever]
        public Role Role { get; set; }
    }
}
