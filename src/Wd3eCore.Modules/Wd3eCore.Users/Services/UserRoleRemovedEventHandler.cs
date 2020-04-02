using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wd3eCore.Security;

namespace Wd3eCore.Users.Services
{
    public class UserRoleRemovedEventHandler : IRoleRemovedEventHandler
    {
        private readonly UserManager<IUser> _userManager;

        public UserRoleRemovedEventHandler(UserManager<IUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task RoleRemovedAsync(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);

            foreach (var user in users)
            {
                await _userManager.RemoveFromRoleAsync(user, roleName);
            }
        }
    }
}
