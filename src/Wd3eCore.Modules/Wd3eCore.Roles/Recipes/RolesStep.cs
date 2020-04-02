using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wd3eCore.Recipes.Models;
using Wd3eCore.Recipes.Services;
using Wd3eCore.Security;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Roles.Recipes
{
    /// <summary>
    /// This recipe step creates a set of roles.
    /// </summary>
    public class RolesStep : IRecipeStepHandler
    {
        private readonly RoleManager<IRole> _roleManager;

        public RolesStep(RoleManager<IRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task ExecuteAsync(RecipeExecutionContext context)
        {
            if (!String.Equals(context.Name, "Roles", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var model = context.Step.ToObject<RolesStepModel>();

            foreach (var importedRole in model.Roles)
            {
                if (string.IsNullOrWhiteSpace(importedRole.Name))
                    continue;

                var role = (Role)await _roleManager.FindByNameAsync(importedRole.Name);
                var isNewRole = role == null;

                if (isNewRole)
                {
                    role = new Role { RoleName = importedRole.Name };
                }

                role.RoleClaims.RemoveAll(c => c.ClaimType == Permission.ClaimType);
                role.RoleClaims.AddRange(importedRole.Permissions.Select(p => new RoleClaim { ClaimType = Permission.ClaimType, ClaimValue = p }));

                if (isNewRole)
                {
                    await _roleManager.CreateAsync(role);
                }
                else
                {
                    await _roleManager.UpdateAsync(role);
                }
            }
        }

        public class RolesStepModel
        {
            public RolesStepRoleModel[] Roles { get; set; }
        }
    }

    public class RolesStepRoleModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Permissions { get; set; }
    }
}
